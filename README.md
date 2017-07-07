# README #

### Objective ###
EMS Data Cacher is a Windows service that is able to utilize the EMS API to deliver detailed institutional/building structure in the form of either XML or JSON.

### Implementation ###
This project was made to be a single add-on to Nomad, delivering building structure in a JSON format.

### Setup ###
This project was developed with Visual Studio (2017), with C#. Simply import this project, compile, and run.
Configuration
* Edit `./EMS Cacher/settings.xml` to set the following parameters:
    * Credentials
    * API Location
    * Output Directory
    * Refresh Interval
    * Timeout
    * Additional building information
    * Logging

### Contact ###
* Email [chand3@vcu.edu](mailto:chand3@vcu.edu) for any inquiries.