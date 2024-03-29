import pandas as pd
import xlsxwriter
import numpy as np
from DataFusion import DataFusion
import time
import datetime
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier
from sklearn.metrics import confusion_matrix
from sklearn.metrics import average_precision_score
from sklearn.metrics import confusion_matrix 
from sklearn.metrics import accuracy_score 
from sklearn.metrics import classification_report 
from sklearn import svm
from sklearn import tree
import matplotlib.pyplot as plt
from sklearn.ensemble import RandomForestClassifier
from sklearn.naive_bayes import GaussianNB
from sklearn.naive_bayes import BernoulliNB
from sklearn.neural_network import MLPClassifier

from sklearn.metrics import precision_recall_curve
from sklearn.metrics import plot_precision_recall_curve
import msgpack as mp
import sys
from sklearn.metrics import precision_recall_fscore_support
import random
import copy


class CoTrainingClassifier(object):

	def __init__(self, clf, clf2=None, p=-1, n=-1, k=30, u = 75):
		self.clf1_ = clf

		#we will just use a copy of clf (the same kind of classifier) if clf2 is not specified
		if clf2 == None:
			self.clf2_ = copy.copy(clf)
		else:
			self.clf2_ = clf2

		#if they only specify one of n or p, through an exception
		if (p == -1 and n != -1) or (p != -1 and n == -1):
			raise ValueError('Current implementation supports either both p and n being specified, or neither')

		self.p_ = p
		self.n_ = n
		self.k_ = k
		self.u_ = u

		random.seed()


	def fit(self, X1, X2, y):

		#we need y to be a numpy array so we can do more complex slicing
		y = np.asarray(y)

		#set the n and p parameters if we need to
		if self.p_ == -1 and self.n_ == -1:
			num_pos = sum(1 for y_i in y if y_i == 1)
			num_neg = sum(1 for y_i in y if y_i == 0)

			n_p_ratio = num_neg / float(num_pos)

			if n_p_ratio > 1:
				self.p_ = 1
				self.n_ = round(self.p_*n_p_ratio)

			else:
				self.n_ = 1
				self.p_ = round(self.n_/n_p_ratio)

		assert(self.p_ > 0 and self.n_ > 0 and self.k_ > 0 and self.u_ > 0)

		#the set of unlabeled samples
		U = [i for i, y_i in enumerate(y) if y_i == -1]

		#we randomize here, and then just take from the back so we don't have to sample every time
		random.shuffle(U)

		#this is U' in paper
		U_ = U[-min(len(U), self.u_):]

		#the samples that are initially labeled
		L = [i for i, y_i in enumerate(y) if y_i != -1]

		#remove the samples in U_ from U
		U = U[:-len(U_)]


		it = 0 #number of cotraining iterations we've done so far

		#loop until we have assigned labels to everything in U or we hit our iteration break condition
		while it != self.k_ and U:
			it += 1

			self.clf1_.fit(X1[L], y[L])
			self.clf2_.fit(X2[L], y[L])

			y1_prob = self.clf1_.predict_proba(X1[U_])
			y2_prob = self.clf2_.predict_proba(X2[U_])

			n, p = [], []

			for i in (y1_prob[:,0].argsort())[-self.n_:]:
				if y1_prob[i,0] > 0.5:
					n.append(i)
			for i in (y1_prob[:,1].argsort())[-self.p_:]:
				if y1_prob[i,1] > 0.5:
					p.append(i)

			for i in (y2_prob[:,0].argsort())[-self.n_:]:
				if y2_prob[i,0] > 0.5:
					n.append(i)
			for i in (y2_prob[:,1].argsort())[-self.p_:]:
				if y2_prob[i,1] > 0.5:
					p.append(i)

			#label the samples and remove thes newly added samples from U_
			y[[U_[x] for x in p]] = 1
			y[[U_[x] for x in n]] = 0

			L.extend([U_[x] for x in p])
			L.extend([U_[x] for x in n])

			U_ = [elem for elem in U_ if not (elem in p or elem in n)]

			#add new elements to U_
			add_counter = 0 #number we have added from U to U_
			num_to_add = len(p) + len(n)
			while add_counter != num_to_add and U:
				add_counter += 1
				U_.append(U.pop())


			#TODO: Handle the case where the classifiers fail to agree on any of the samples (i.e. both n and p are empty)


		#let's fit our final model
		self.clf1_.fit(X1[L], y[L])
		self.clf2_.fit(X2[L], y[L])


	#TODO: Move this outside of the class into a util file.
	def supports_proba(self, clf, x):
		"""Checks if a given classifier supports the 'predict_proba' method, given a single vector x"""
		try:
			clf.predict_proba([x])
			return True
		except:
			return False

	def predict(self, X1, X2):
		y1 = self.clf1_.predict(X1)
		y2 = self.clf2_.predict(X2)

		proba_supported = self.supports_proba(self.clf1_, X1[0]) and self.supports_proba(self.clf2_, X2[0])

		#fill y_pred with -1 so we can identify the samples in which the classifiers failed to agree
		y_pred = np.asarray([-1] * X1.shape[0])

		for i, (y1_i, y2_i) in enumerate(zip(y1, y2)):
			if y1_i == y2_i:
				y_pred[i] = y1_i
			elif proba_supported:
				y1_probs = self.clf1_.predict_proba([X1[i]])[0]
				y2_probs = self.clf2_.predict_proba([X2[i]])[0]
				sum_y_probs = [prob1 + prob2 for (prob1, prob2) in zip(y1_probs, y2_probs)]
				max_sum_prob = max(sum_y_probs)
				y_pred[i] = sum_y_probs.index(max_sum_prob)

			else:
				#the classifiers disagree and don't support probability, so we guess
				y_pred[i] = random.randint(0, 1)


		#check that we did everything right
		assert not (-1 in y_pred)

		return y_pred


	def predict_proba(self, X1, X2):
		"""Predict the probability of the samples belonging to each class."""
		y_proba = np.full((X1.shape[0], 2), -1, np.float)

		y1_proba = self.clf1_.predict_proba(X1)
		y2_proba = self.clf2_.predict_proba(X2)

		for i, (y1_i_dist, y2_i_dist) in enumerate(zip(y1_proba, y2_proba)):
			y_proba[i][0] = (y1_i_dist[0] + y2_i_dist[0]) / 2
			y_proba[i][1] = (y1_i_dist[1] + y2_i_dist[1]) / 2

		_epsilon = 0.0001
		assert all(abs(sum(y_dist) - 1) <= _epsilon for y_dist in y_proba)
		return y_proba

def get_intrusion_window(adversary_path,to_monitor):
    adversary_path='C:/Users/substationc/Desktop/DataFusion/DataFusionApp/DataFusionApp/PythonScripts/'+adversary_path
    fusion = DataFusion()
    fusion.load_json(adversary_path)
    fusion.extract_cyber_data()
    fusion.extract_physical_data_with_values(to_monitor)
    data_to_process = fusion.merge()
    attack_start = data_to_process.iloc[0]['Time']
    start = int(time.mktime(attack_start.timetuple()))
    attack_end = data_to_process.iloc[-1]['Time']
    end = int(time.mktime(attack_end.timetuple()))
    return start,end

def supervised_learning(_usecase,_os,_poll_rate, to_monitor,pca, pure_cyber=False, pure_phy=False):
    usecase=_usecase
    os=_os
    poll_rate = _poll_rate

    if os==10 and poll_rate ==60 and usecase=='UC1':
        path='csvs/UC1/DS_merged_phy_cyb_10os_60poll_encoded_uc1.csv'
        adv_path='Adversary/UC1_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC1':
        path='csvs/UC1/DS_merged_phy_cyb_10os_30poll_encoded_uc1.csv'
        adv_path='Adversary/UC1_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC2':
        path='csvs/UC2/uc2_DS_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC2':
        path='csvs/UC2/uc2_DS_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC2':
        path='csvs/UC2/uc2_DS_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC2':
        path='csvs/UC2/uc2_DS_merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC3':
        path='csvs/UC3/uc3_DS_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC3':
        path='csvs/UC3/uc3_DS_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC3':
        path='csvs/UC3/uc3_DS_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC3':
        path='csvs/UC3/uc3_DS_merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC4':
        path='csvs/UC4/uc4_DS_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC4':
        path='csvs/UC4/uc4_DS_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC4':
        path='csvs/UC4/uc4_DS_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC4':
        path='csvs/UC4/uc4_DS_merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'


    start_time,end_time = get_intrusion_window(adv_path,to_monitor)
    path='C:/Users/substationc/Desktop/DataFusion/DataFusionApp/DataFusionApp/PythonScripts/'+path
    data = pd.read_csv(path)


    #data.drop('Unnamed:0',1)
    data = data.drop(data.columns[[0]], axis=1)
    data['DNP3 Objects'].replace('None', np.nan, inplace=True)

    replace_map = dict([('DNP3 Objects',0),('value1', 0.0), ('value2', 0.0), ('value3', 0.0), 
                   ('value4', 0.0),('value5',0.0)])

    # fill nan by replace values
    data = data.fillna(value=replace_map)

    data['Time'] = pd.to_datetime(data['Time'])

    data['Label'] = 0
    for i,val in data.iterrows():
        unix_time = int(time.mktime(val['Time'].timetuple()))
        if unix_time <end_time and unix_time>start_time:
            data['Label'][i] = 1

    # compute the feature table
    feature_table = data.drop(columns=['Time', 'snort_alert', 'snort_alert_type','Label'])
    if pure_cyber:
        feature_table = data.drop(columns=['Time', 'snort_alert', 'snort_alert_type','Label','LL_dnp3_src','LL_dnp3_dst'
                                           ,'LL_dnp3_len','LL_dnp3_ctl','TL_dnp3_tr_ctl','AL_dnp3_al_func','AL_dnp3_al_ctl'
                                           ,'DNP3 Object Count','DNP3 Objects','AL_Payload'])
        
        # drop physical value features
        feature_table = feature_table[feature_table.columns[~feature_table.columns.str.contains('value')]]
        print(feature_table.columns)
    if pure_phy:
        feature_table = data.drop(columns=['Time', 'snort_alert', 'snort_alert_type','frame_len','frame_protocols','eth_src','eth_dst'
                                           ,'ip_src','ip_dst','ip_len','ip_flags','tcp_srcport','tcp_dstport','tcp_len'
                                           ,'tcp_flags','tcp_retransmission','tcp_rtt','flow_count','flow_final_count','packets','Label'])

    feature_array = feature_table.to_numpy()
    label_array = data[['Label']].to_numpy().flatten()
    
    X= feature_array
    y= label_array
    
    N_SAMPLES = feature_table.shape[0]
    N_FEATURES = feature_table.shape[1]
    
    y[:N_SAMPLES//2] = -1

    X_test = X[-N_SAMPLES//4:]
    y_test = y[-N_SAMPLES//4:]

    X_labeled = X[N_SAMPLES//2:-N_SAMPLES//4]
    y_labeled = y[N_SAMPLES//2:-N_SAMPLES//4]

    y = y[:-N_SAMPLES//4]
    X = X[:-N_SAMPLES//4]

    X1 = X[:,:N_FEATURES//2]
    X2 = X[:,N_FEATURES//2:]
    
    # using panda dataframe to store the probability scores to be used later on in the DS theory paper
    prob_table = pd.DataFrame()
    #score_table = pd.DataFrame()

    # if pca
    if (pca):
        # Now use PCA for dimensional reduction and reperform the supervised learning
        from sklearn.decomposition import PCA
        pca = PCA(n_components=10)
        pca.fit(feature_table.values)

        pca_result = pca.transform(feature_table.values)
        pca_table1 = pd.DataFrame(columns=['f1', 'f2', 'f3', 'f4', 'f5','f6', 'f7', 'f8', 'f9', 'f10'])
        for i in range(10):
            pca_table1[f'f{i+1}'] = pca_result[:,i]

        pca_feature_array = pca_table1.to_numpy()
        
        X= pca_feature_array
        y= label_array
        
        N_SAMPLES = feature_table.shape[0]
        N_FEATURES = feature_table.shape[1]
    
        y[:N_SAMPLES//2] = -1

        X_test = X[-N_SAMPLES//4:]
        y_test = y[-N_SAMPLES//4:]

        X_labeled = X[N_SAMPLES//2:-N_SAMPLES//4]
        y_labeled = y[N_SAMPLES//2:-N_SAMPLES//4]

        y = y[:-N_SAMPLES//4]
        X = X[:-N_SAMPLES//4]

        X1 = X[:,:N_FEATURES//2]
        X2 = X[:,N_FEATURES//2:]
    

    #clf = KNeighborsClassifier()
    #clf.fit(X_train, y_train)
    #predictions = clf.predict(X_test)
    
    #res_knn = precision_recall_fscore_support(y_test, predictions, average='weighted')   
    #probs = clf.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['knn'] = probs
    #prob_table['knn'] = res_knn
 
    clf = CoTrainingClassifier(svm.SVC(probability=True), u=N_SAMPLES//10)
    clf.fit(X1, X2, y)
    y_pred = clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_svc = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = clf.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['svc'] = probs
    prob_table['svc'] = res_svc
    
    
    dt_co_clf = CoTrainingClassifier(tree.DecisionTreeClassifier(), u=N_SAMPLES//10)
    dt_co_clf.fit(X1, X2, y)
    y_pred = dt_co_clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_dt = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = dt.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['dt'] = probs
    prob_table['dt'] = res_dt
    
    
    rf_co_clf = CoTrainingClassifier(RandomForestClassifier(n_estimators=10), u=N_SAMPLES//10)
    rf_co_clf.fit(X1, X2, y)
    y_pred = rf_co_clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_rf = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = rf.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['rf'] = probs
    prob_table['rf'] = res_rf
    
    gnb_co_clf = CoTrainingClassifier(GaussianNB(), u=N_SAMPLES//10)
    gnb_co_clf.fit(X1, X2, y)
    y_pred = gnb_co_clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_gnb = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = gnb.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['gnb'] = probs
    prob_table['gnb'] = res_gnb
    
    bnb_co_clf = CoTrainingClassifier(BernoulliNB(), u=N_SAMPLES//10)
    bnb_co_clf.fit(X1, X2, y)
    y_pred = bnb_co_clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_bnb = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = bnb.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['bnb'] = probs
    prob_table['bnb'] = res_bnb
    
    
    nn_co_clf = CoTrainingClassifier(MLPClassifier(solver='lbfgs', alpha=1e-5,hidden_layer_sizes=(5, 2), random_state=1), u=N_SAMPLES//10)
    nn_co_clf.fit(X1, X2, y)
    y_pred = nn_co_clf.predict(X_test[:, :N_FEATURES // 2], X_test[:, N_FEATURES // 2:])
    res_nn = precision_recall_fscore_support(y_test, y_pred, average='weighted')
    #probs = nn.predict_proba(X_test)
    #probs = probs[:, 1]
    #score_table['nn'] = probs
    prob_table['mlp'] = res_nn
    #prob_table = prob_table.drop(columns=['Time'])
    return prob_table
    #return prob_table,score_table
    
case = sys.argv[1]
enable_PCA = sys.argv[2]
pc = sys.argv[3]
pp = sys.argv[4]
_usecase = case.split('_')[0]
print(_usecase)
outstations = case.split('_')[1]
_os = outstations.replace('OS','')
poll_interval = case.split('_')[2]
_pi = poll_interval.replace('poll','')
to_monitor={}
if 'UC1' in _usecase:
    to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
elif 'UC2' in _usecase:
    to_monitor ={'390':[20],'601':[34],'968':[27],'968':[29]}
elif 'UC3' in _usecase:
    to_monitor ={'390':[20],'560':[24], '601':[34],'968':[27],'968':[29]}
elif 'UC4' in _usecase:
    to_monitor ={'390':[20],'601':[38], '601':[38],'968':[27],'968':[29]}
    
data_as_df = supervised_learning(_usecase,int(_os),int(_pi),to_monitor,pca=enable_PCA,pure_cyber= pc, pure_phy=pp)
data_as_list = data_as_df.values.tolist()
#score_as_list = score_as_df.values.tolist()
mp.pack(data_as_list, open('cotrain_pscores_'+sys.argv[1]+'.mp','wb'))
#mp.pack(score_as_list, open('prob_'+sys.argv[1]+'.mp','wb'))
