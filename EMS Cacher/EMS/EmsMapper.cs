using EducationalInstitution;
using Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML;
using Data;
using static Persistence;

namespace EMS_Cacher
{
    class EmsMapper
    {
        public static University mapUniversity()
        {
            University university = new University(config.getObject("University"));
            List<Campus> campuses = new List<Campus>();
            List<XMLElement> buildings = XMLElement.inflate(                            // Inflate the buildings into XML
                new SoapClient()                                                        // Initialize the Soap client
                    .setURL(config.getString("URL"))                                    // Set the url to our destination
                    .setRequest(new List<XMLElement>() {                                //
                        new XMLElement("GetBuildings")                                  // Build the request and configure API
                            .setAttribute("xmlns", "http://DEA.EMS.API.Web.Service/")   // 
                            .append(new XMLElement("UserName")                          //
                                .text(config.getString("Username")))                    // Supply the API's username
                            .append(new XMLElement("Password")                          //
                                .text(config.getString("Password")))                    // Supply the API's password
                    })                                                                  //
                    .send()                                                             // Send the response
                    .getResponse()                                                      // Get the response in the form of an XMLDocument
                    .root()                                                             // Get the body of the XMLDocument (XMLElement)
                    .getElementByTagName("GetBuildingsResult")                          // Get the result (XMLElement)
                    .text()                                                             // Get the innerText of the result
            )[0].children();                                                            // Get array of children
            console.log("Obtained " + buildings.Count + " buildings.");
            foreach (XMLElement building in buildings)
            {
                string[] name = building.getElementByTagName("Description").text().Split('|');
                string campusName = name.Length == 1 ? "Other" : name[0].Trim();
                string buildingName = name.Length == 1 ? name[0].Trim() : name[1].Trim();
                if (!university.hasCampus(campusName))
                {
                    university.addCampus(new Campus(campusName));
                }
                console.log("Getting Rooms for " + buildingName);
                university.getCampus(campusName).addBuilding(new Building(
                    buildingName,
                    building.getElementByTagName("BuildingCode").text(),
                    Int32.Parse(building.getElementByTagName("ID").text())
                ).addRooms(getRooms(
                    Int32.Parse(building.getElementByTagName("ID").text())
                )));
            }
            return university;
        }
        public static List<Room> getRooms(int buildingID)
        {
            string response = new SoapClient()                                      // Initialize the Soap client
                .setURL(config.getString("URL"))                                    // Set the url to our destination
                .setRequest(new List<XMLElement>() {                                //
                    new XMLElement("GetAllRooms")                                  // Build the request and configure API
                        .setAttribute("xmlns", "http://DEA.EMS.API.Web.Service/")   // 
                        .append(new XMLElement("UserName")                          //
                            .text(config.getString("Username")))                    // Supply the API's username
                        .append(new XMLElement("Password")                          //
                            .text(config.getString("Password")))                    // Supply the API's password
                        .append(new XMLElement("BuildingID")                        //
                            .text(buildingID.ToString()))                           // Set which building to get rooms from
                })                                                                  //
                .send()                                                             // Send the response
                .getResponse()                                                      // Get the response in the form of an XMLDocument
                .root()                                                             // Get the body of the XMLDocument (XMLElement)
                .getElementByTagName("GetAllRoomsResponse")                         // Get the result (XMLElement)
                .text();                                                            // Get the innerText of the result
            List<XMLElement> xmlRooms = XMLElement.inflate(response)[0].children(); // Inflate the buildings into XML
            List<Room> rooms = new List<Room>();
            foreach (XMLElement room in xmlRooms)
            {
                string num = room.getElementByTagName("Room").text();
                string roomNumber = "0";
                for (int i = 0; i != num.Length; i++)
                {
                    if ('0' <= num[i] && num[i] <= '9')
                    {
                        roomNumber += num[i];
                    }
                }
                rooms.Add(new Room(Int32.Parse(roomNumber),
                    room.getElementByTagName("Description").text(),
                    Int32.Parse(room.getElementByTagName("ID").text())));
            }
            return rooms;
        }
    }
}
