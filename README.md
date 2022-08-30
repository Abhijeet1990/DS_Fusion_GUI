# DS_Fusion_GUI
This is an application for multi-sensor data fusion alongwith the integration of location-based fusion using Dempster Shafer Theory.

The publications related to the following works are:
- [Multi-Source Multi-Domain Data Fusion for Cyberattack Detection in Power Systems](https://ieeexplore.ieee.org/abstract/document/9521204) ,IEEE Access, Aug 2021
- [Inter-Domain Fusion for Enhanced Intrusion Detection in Power Systems: An Evidence Theoretic and Meta-Heuristic Approach](https://www.mdpi.com/1424-8220/22/6/2100) ,MDPI Sensors, Feb 2022

**Description of the files and folders within `PythonScripts` folder**
- `DataFusion.py` : This is the main class for collecting information from different sensors. `extract_pcap()` function extracts Round Trip Time and TCP Retransmission information using *Pyshark*. `extract_physical_data()` extracts the DNP3 header information, while `extract_physical_data_with_values()` extracts the DNP3 Point information from the pcaps. `extract_single_layer()` individually extracts the DNP3 Application, Transport and Link Layer information. The function code specific 
functions are: `extract_dnp3_read_request()`, `extract_dnp3_direct_operate()`, `extract_dnp3_response()`, etc. The `extract_cyber_data()` function extracts the cyber information from the Network, Transport, Link layer headers. `extract_packetbeat()` connects the *Packetbeat* index within *ElasticSearch*. `merge_packetbeat()` merges the flow-based information from Packetbeat to the cyber features.
`extract_snort()`,`process_snort()` and `merge_Snort()` are the functions to extract Snort IDS alerts and merge with the cyber feature space. `merge_by_location()` merges cyber and physical features based on the timestamps. `merge_by_location()` merges the cyber and physical features based on packet captured at different location in the network.

- `CoTrainedScores.py` : This is semi-supervised learning classifier used for intrusion detection. The codes are implemented for different use-cases.
- `GetData.py` : This is the code for extracting the data into the front end application. 
- `GetStepData.py` : This is the code for extracting the data sequentially at evert stage of feature transformation. 
- `GetTrainedScores.py` : Code for IDS training with supervised learning based classifier.

