# Get Data based on the use case

import pandas as pd
import numpy as np
from DataFusionModifiedAttackProbability import DataFusion
import time
import datetime
import msgpack as mp
import sys

def get_intrusion_window(adversary_path):
    adversary_path='C:/Users/substationc/Desktop/DataFusion/DataFusionApp/DataFusionApp/PythonScripts'+adversary_path
    fusion = DataFusion()
    fusion.load_json(adversary_path)
    fusion.extract_cyber_data()
    fusion.extract_physical_data()
    data_to_process = fusion.merge()
    attack_start = data_to_process.iloc[0]['Time']
    start = int(time.mktime(attack_start.timetuple()))
    attack_end = data_to_process.iloc[-1]['Time']
    end = int(time.mktime(attack_end.timetuple()))
    return start,end
    
def get_lucene_query(start_time, end_time):
    json_body = {
    "query": {
        "bool": {
            "must": [
                {
                    "range": {
                        "event.end": {
                            "gte": start_time,
                            "lte": end_time
                        }
                    }
                },
                {"range": {
                    "event.duration": {
                        "gte": 0,
                        "lte": 3000000
                    }
                }},
                {"bool": {"should": [
                    {"match": {
                        "destination.port": "20000"
                    }}
                    , {
                        "match": {
                            "source.port": "20000"
                        }
                    }
                ]
                }

                }
                    ]
                }
            }
        }
    return json_body


def get_file_path(_usecase,_os,_poll_rate):
    usecase=_usecase
    os=_os
    poll_rate = _poll_rate
    start_time='2020-10-17T12:28:00.000Z'
    end_time='2020-10-17T20:45:00.000Z'
    common_path='C:/Users/substationc/Desktop/DataFusion/DataFusionApp/DataFusionApp/PythonScripts/'
    if os==10 and poll_rate ==60 and usecase=='UC1':
        jsonpath='Raw/Uc1_master_10OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll60_uc1.pickle'
        snort_path='ProcessedFiles/UC1_PyDNP3_CORE_Snort_10_OS_60_1017'
        start_time='2020-10-17T17:45:00.000Z'
        end_time='2020-10-17T18:30:00.000Z'
    elif os==10 and poll_rate ==30 and usecase=='UC1':
        jsonpath='Raw/Uc1_master_10OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_Poll30_uc1.pickle'
        snort_path='ProcessedFiles/UC1_PyDNP3_CORE_Snort_10_OS_30_1017'
        start_time='2020-10-17T15:30:00.000Z'
        end_time='2020-10-17T16:15:00.000Z'
    elif os==5 and poll_rate ==30 and usecase=='UC2':
        jsonpath='Raw/Uc2_master_5OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll30_uc2.pickle'
        snort_path='ProcessedFiles/UC2_PyDNP3_CORE_Snort_5_OS_30_1017'
        start_time='2020-10-17T17:50:00.000Z'
        end_time='2020-10-17T18:30:00.000Z'
    elif os==5 and poll_rate ==60 and usecase=='UC2':
        jsonpath='Raw/Uc2_master_5OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll60_uc2.pickle'
        snort_path='ProcessedFiles/UC2_PyDNP3_CORE_Snort_5_OS_60_1017'
        start_time='2020-10-17T19:35:00.000Z'
        end_time='2020-10-17T20:00:00.000Z'
    elif os==10 and poll_rate ==30 and usecase=='UC2':
        jsonpath='Raw/Uc2_master_10OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll30_uc2.pickle'
        snort_path='ProcessedFiles/UC2_PyDNP3_CORE_Snort_10_OS_30_1017'
        start_time='2020-10-17T15:50:00.000Z'
        end_time='2020-10-17T16:30:00.000Z'
    elif os==10 and poll_rate ==60 and usecase=='UC2':
        jsonpath='Raw/Uc2_master_10OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll60_uc2.pickle'
        snort_path='ProcessedFiles/UC2_PyDNP3_CORE_Snort_10_OS_60_1017'
        start_time='2020-10-17T18:15:00.000Z'
        end_time='2020-10-17T18:45:00.000Z'
    elif os==5 and poll_rate ==30 and usecase=='UC3':
        jsonpath='Raw/Uc3_master_5OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll30_uc3.pickle'
        snort_path='ProcessedFiles/UC3_PyDNP3_CORE_Snort_5_OS_30_1017'
        start_time='2020-10-17T17:40:00.000Z'
        end_time='2020-10-17T18:05:00.000Z'
    elif os==5 and poll_rate ==60 and usecase=='UC3':
        jsonpath='Raw/Uc3_master_5OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll60_uc3.pickle'
        snort_path='ProcessedFiles/UC3_PyDNP3_CORE_Snort_5_OS_60_1017'
        start_time='2020-10-17T19:15:00.000Z'
        end_time='2020-10-17T19:45:00.000Z'
    elif os==10 and poll_rate ==30 and usecase=='UC3':
        jsonpath='Raw/Uc3_master_10OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll30_uc3.pickle'
        snort_path='ProcessedFiles/UC3_PyDNP3_CORE_Snort_10_OS_30_1017'
        start_time='2020-10-17T16:15:00.000Z'
        end_time='2020-10-17T16:50:00.000Z'
    elif os==10 and poll_rate ==60 and usecase=='UC3':
        jsonpath='Raw/Uc3_master_10OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll60_uc3.pickle'
        snort_path='ProcessedFiles/UC3_PyDNP3_CORE_Snort_10_OS_60_1017'
        start_time='2020-10-17T18:30:00.000Z'
        end_time='2020-10-17T19:00:00.000Z'
    elif os==5 and poll_rate ==30 and usecase=='UC4':
        jsonpath='Raw/Uc4_master_5OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll30_uc4.pickle'
        snort_path='ProcessedFiles/UC4_PyDNP3_CORE_Snort_5_OS_30_1017'
        start_time='2020-10-17T17:25:00.000Z'
        end_time='2020-10-17T17:55:00.000Z'
    elif os==5 and poll_rate ==60 and usecase=='UC4':
        jsonpath='Raw/Uc4_master_5OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_5os_poll60_uc4.pickle'
        snort_path='ProcessedFiles/UC4_PyDNP3_CORE_Snort_5_OS_60_1017'
        start_time='2020-10-17T19:00:00.000Z'
        end_time='2020-10-17T19:30:00.000Z'
    elif os==10 and poll_rate ==30 and usecase=='UC4':
        jsonpath='Raw/Uc4_master_10OS_Poll30_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll30_uc4.pickle'
        snort_path='ProcessedFiles/UC4_PyDNP3_CORE_Snort_10_OS_30_1017'
        start_time='2020-10-17T17:05:00.000Z'
        end_time='2020-10-17T17:35:00.000Z'
    elif os==10 and poll_rate ==60 and usecase=='UC4':
        jsonpath='Raw/Uc4_master_10OS_Poll60_dnp3_arp.json'
        pickle_path='PickleFiles/master_dnp3_arp_10os_poll60_uc4.pickle'
        snort_path='ProcessedFiles/UC4_PyDNP3_CORE_Snort_10_OS_60_1017'
        start_time='2020-10-17T18:40:00.000Z'
        end_time='2020-10-17T19:15:00.000Z'
    return common_path+jsonpath, common_path+pickle_path, common_path+snort_path, start_time, end_time
    
    

print('Argument List:' + str(sys.argv))
case = sys.argv[1]
_usecase = case.split('_')[0]
print(_usecase)
outstations = case.split('_')[1]
_os = outstations.replace('OS','')
poll_interval = case.split('_')[2]
_pi = poll_interval.replace('poll','')
stage = sys.argv[2]

#### based on the stage pack the value of that stage and return####
jsonpath, pickle_path, snort_path,start_time, end_time = get_file_path(_usecase,int(_os),int(_pi))

fusion = DataFusion()
fusion.load_json(jsonpath)
fusion.extract_cyber_data()
data_as_list=[]
to_monitor={}
if 'UC1' in _usecase:
    to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
elif 'UC2' in _usecase:
    to_monitor ={'390':[20],'601':[34],'631':[23],'968':[27],'968':[29]}
elif 'UC3' in _usecase:
    to_monitor ={'390':[20],'560':[24], '601':[34],'968':[27],'968':[29]}
elif 'UC4' in _usecase:
    to_monitor ={'390':[20],'601':[38], '601':[38],'968':[27],'968':[29]}
if stage == '1':
    data_as_list = fusion.cyber_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage == '2':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    data_as_list = fusion.cyber_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage =='3':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    data_as_list = fusion.cyber_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage == '4':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    fusion.process_snort(snort_path)
    fusion.merge_snort()
    data_as_list = fusion.cyber_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage == '5':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    fusion.process_snort(snort_path)
    fusion.merge_snort()
    #to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
    fusion.physical_table = fusion.extract_physical_data_with_values(to_monitor) # have to add the function in the class
    data_as_list = fusion.physical_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage == '6':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    fusion.process_snort(snort_path)
    fusion.merge_snort()
    #to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
    fusion.physical_table = fusion.extract_physical_data_with_values(to_monitor) # have to add the function in the class
    fusion.merge()
    fusion.merged_table = fusion.merged_table.drop(columns=['Time'])
    data_as_list = fusion.merged_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()   
elif stage == '7':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    fusion.process_snort(snort_path)
    fusion.merge_snort()
    #to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
    fusion.physical_table = fusion.extract_physical_data_with_values(to_monitor) # have to add the function in the class
    fusion.merge()
    replace_map = [('ip.src', '0.0.0.0'), ('ip.dst', '0.0.0.0'), ('ip.len', 0),
               ('ip.flags', '0x00000000'),('tcp.srcport', 0), ('tcp.dstport', 0),('tcp.flags', '0x00000000'),
               ('tcp.len', 0),('LL_dnp3_src', -1), ('LL_dnp3_dst', -1),('LL_dnp3_len', 0), ('AL_dnp3_al_func', -1),
              ('LL_dnp3_ctl', '0x00000000'),('TL_dnp3_tr_ctl', '0x00000000'),
               ('AL_dnp3_al_ctl', '0x00000000'),('AL_dnp3_obj', 0), ('AL_Payload', 0),
              ('DNP3 Object Count', 0),('DNP3 Objects', -1), ('tcp_rtt', -1), ('tcp_retransmission', 0),
              ('snort_alert', 0),('snort_alert_type', 'None'), ('flow.count', -1), ('flow.final_count', -1),
              ('packets', -1)]
    replace_map = dict(replace_map)
    fusion.imputate(replace_map)
    fusion.merged_table = fusion.merged_table.drop(columns=['Time'])
    data_as_list = fusion.merged_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
elif stage == '8':
    fusion.pcap_table = pd.read_pickle(pickle_path)
    fusion.merge_by_pcap()
    fusion.connect_to_elasticsearch('10.110.215.39')
    json_body=get_lucene_query(start_time, end_time)
    fusion.retrieve_packetbeat(json_body = json_body)
    fusion.extract_packetbeat()
    fusion.merge_packetbeat()
    fusion.process_snort(snort_path)
    fusion.merge_snort()
    #to_monitor ={'399':[5], '456':[18],'1195':[24],'1200':[27]}
    fusion.physical_table = fusion.extract_physical_data_with_values(to_monitor) # have to add the function in the class
    fusion.merge()
    replace_map =[('ip.src', '0.0.0.0'), ('ip.dst', '0.0.0.0'), ('ip.len', 0),
               ('ip.flags', '0x00000000'),('tcp.srcport', 0), ('tcp.dstport', 0),('tcp.flags', '0x00000000'),
               ('tcp.len', 0),('LL_dnp3_src', -1), ('LL_dnp3_dst', -1),('LL_dnp3_len', 0), ('AL_dnp3_al_func', -1),
              ('LL_dnp3_ctl', '0x00000000'),('TL_dnp3_tr_ctl', '0x00000000'),
               ('AL_dnp3_al_ctl', '0x00000000'),('AL_dnp3_obj', 0), ('AL_Payload', 0),
              ('DNP3 Object Count', 0),('DNP3 Objects', -1), ('tcp_rtt', -1), ('tcp_retransmission', 0),
              ('snort_alert', 0),('snort_alert_type', 'None'), ('flow.count', -1), ('flow.final_count', -1),
              ('packets', -1)]
    replace_map = dict(replace_map)
    fusion.imputate(replace_map)
    encoding_list=['frame.protocols', 'eth.src', 'eth.dst', 'ip.src',
       'ip.dst', 'ip.len', 'ip.flags', 'tcp.srcport', 'tcp.dstport', 'tcp.len',
       'tcp.flags', 'snort_alert_type', 'LL_dnp3_src', 'LL_dnp3_dst', 'LL_dnp3_len', 'LL_dnp3_ctl',
       'TL_dnp3_tr_ctl', 'AL_dnp3_al_ctl', 'AL_dnp3_obj',
       'AL_Payload']
    fusion.encode(encoding_list)
    fusion.merged_table = fusion.merged_table.drop(columns=['Time'])
    data_as_list = fusion.merged_table.values.tolist()
    mp.pack(data_as_list, open('msgpack_'+sys.argv[1]+'_'+stage+'.mp','wb'))
    sys.exit()
    
    
    
    
