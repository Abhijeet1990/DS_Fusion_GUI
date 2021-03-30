# Get Data based on the use case

import pandas as pd
import numpy as np
from DataFusionModifiedAttackProbability import DataFusion
import time
import datetime
import msgpack as mp
import sys

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

def get_data(_usecase,_os,_poll_rate, to_monitor):
    usecase=_usecase
    os=_os
    poll_rate = _poll_rate

    if os==10 and poll_rate ==60 and usecase=='UC1':
        path='csvs/UC1/merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC1_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC1':
        path='csvs/UC1/merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC1_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC2':
        path='csvs/UC2/uc2_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC2':
        path='csvs/UC2/uc2_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC2':
        path='csvs/UC2/uc2_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC2':
        path='csvs/UC2/uc2_merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC2_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC3':
        path='csvs/UC3/uc3_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC3':
        path='csvs/UC3/uc3_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC3':
        path='csvs/UC3/uc3_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC3':
        path='csvs/UC3/uc3_merged_phy_cyb_10os_60poll_encoded.csv'
        adv_path='Adversary/UC3_PyDNP3_CORE_Adversary_10_OS_60_dnp3.json'
    elif os==5 and poll_rate ==30 and usecase=='UC4':
        path='csvs/UC4/uc4_merged_phy_cyb_5os_30poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_5_OS_30_dnp3.json'
    elif os==5 and poll_rate ==60 and usecase=='UC4':
        path='csvs/UC4/uc4_merged_phy_cyb_5os_60poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_5_OS_60_dnp3.json'
    elif os==10 and poll_rate ==30 and usecase=='UC4':
        path='csvs/UC4/uc4_merged_phy_cyb_10os_30poll_encoded.csv'
        adv_path='Adversary/UC4_PyDNP3_CORE_Adversary_10_OS_30_dnp3.json'
    elif os==10 and poll_rate ==60 and usecase=='UC4':
        path='csvs/UC4/uc4_merged_phy_cyb_10os_60poll_encoded.csv'
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
    
    data = data.drop(columns=['Time'])
    return data
    
    

print('Argument List:' + str(sys.argv))
case = sys.argv[1]
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
    to_monitor ={'390':[20],'601':[34],'631':[23],'968':[27],'968':[29]}
elif 'UC3' in _usecase:
    to_monitor ={'390':[20],'560':[24], '601':[34],'968':[27],'968':[29]}
elif 'UC4' in _usecase:
    to_monitor ={'390':[20],'601':[38], '601':[38],'968':[27],'968':[29]}
    
data_as_df = get_data(_usecase,int(_os),int(_pi),to_monitor)
data_as_list = data_as_df.values.tolist()
mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'.mp','wb'))