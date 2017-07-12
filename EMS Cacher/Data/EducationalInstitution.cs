using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace EducationalInstitution
{
    public class University : Serializable.Object
    {
        private Serializable.Array m_campuses = new Serializable.Array();
        private string m_name;
        private string m_acronym;
        private string m_department;

        public University(Serializable.Object props)
        {
            base.apply(props);
            base.set("Campuses", m_campuses);
            this.m_name = props.getString("Name");
            this.m_acronym = props.getString("Acronym");
            this.m_department = props.getString("Department");
        }
        public University addCampus(Campus campus)
        {
            m_campuses.add(campus);
            return this;
        }
        public University addCampuses(List<Campus> campuses)
        {
            foreach (Campus campus in campuses)
            {
                m_campuses.add(campus);
            }
            return this;
        }
        public string getName()
        {
            return this.m_name;
        }
        public string getAcronym()
        {
            return this.m_acronym;
        }
        public string getDepartment()
        {
            return this.m_department;
        }
        public Campus getCampus(string name)
        {
            var b = m_campuses.getChildren();
            foreach (var i in b)
            {
                Campus building = (Campus)i.Item2;
                if (building.getName() == name)
                {
                    return building;
                }
            }
            return null;
        }
        public Campus searchCampus(string name)
        {
            var b = m_campuses.getChildren();
            foreach (var i in b)
            {
                Campus building = (Campus)i.Item2;
                if (building.getName().Contains(name))
                {
                    return building;
                }
            }
            return null;
        }
        public bool hasCampus(string name)
        {
            return this.getCampus(name) != null;
        }
        public List<Campus> getCampuses()
        {
            List<Campus> buildings = new List<Campus>(m_campuses.size());
            for (int i = 0; i != m_campuses.size(); i++)
            {
                buildings.Add((Campus)m_campuses.get(i));
            }
            return buildings;
        }
        public override Object apply(Object genericObj)
        {
            if (genericObj != null && genericObj.getType() == "object")
            {
                Serializable.Object obj = (Serializable.Object)genericObj;
                /*
                 * @FIXME: Wildcard detection doesn't work
                 */
                Serializable.Object universityObj = obj.getObject("*");
                if (obj.getObject(this.m_name) != null)
                {
                    universityObj = obj.getObject(this.m_name);
                }
                else if (obj.getObject(this.m_acronym) != null)
                {
                    universityObj = obj.getObject(this.m_acronym);
                }
                if (universityObj != null)
                {
                    foreach (var i in universityObj.getChildren())
                    {
                        if (i.Item2.getType() == "object")
                        {
                            Campus campus = this.getCampus(i.Item1);
                            if (campus != null)
                            {
                                campus.apply(universityObj);
                                campus.trim(m_campuses);
                            }
                        }
                        else
                        {
                            if (i.Item1 == "Name")
                            {
                                m_name = i.Item2.getValue();
                            }
                            else if (i.Item1 == "Acronym")
                            {
                                m_acronym = i.Item2.getValue();
                            }
                            base.set(i.Item1, new Serializable.String(i.Item2.getValue()));
                        }
                    }
                }
            }
            return this;
        }
    }

    public class Campus : Serializable.Object
    {
        private Serializable.Array m_buildings = new Serializable.Array();
        private string m_name;

        public Campus(string name)
        {
            base.set("Name", new Serializable.String(name));
            base.set("Buildings", m_buildings);
            this.m_name = name;
        }
        public Campus addBuilding(Building building)
        {
            m_buildings.add(building);
            return this;
        }
        public Campus addBuildings(List<Building> buildings)
        {
            foreach (Building building in buildings)
            {
                m_buildings.add(building);
            }
            return this;
        }
        public string getName()
        {
            return this.m_name;
        }
        public Building getBuilding(string name)
        {
            var b = m_buildings.getChildren();
            foreach (var i in b)
            {
                Building building = (Building)i.Item2;
                if (building.getAcronym() == name || building.getName() == name)
                {
                    return building;
                }
            }
            return null;
        }
        public Building searchBuilding(string name)
        {
            var b = m_buildings.getChildren();
            foreach (var i in b)
            {
                Building building = (Building)i.Item2;
                if (building.getAcronym().Contains(name) || building.getName().Contains(name))
                {
                    return building;
                }
            }
            return null;
        }
        public Building getBuilding(int id)
        {
            var b = m_buildings.getChildren();
            foreach (var i in b)
            {
                Building building = (Building)i.Item2;
                if (building.getID() == id)
                {
                    return building;
                }
            }
            return null;
        }
        public bool hasBuilding(string name)
        {
            return this.getBuilding(name) != null;
        }
        public bool hasBuilding(int id)
        {
            return this.getBuilding(id) != null;
        }
        public List<Building> getBuildings()
        {
            List<Building> buildings = new List<Building>(m_buildings.size());
            for (int i = 0; i != m_buildings.size(); i++)
            {
                buildings.Add((Building) m_buildings.get(i));
            }
            return buildings;
        }
        public override Object apply(Object genericObj)
        {
            if (genericObj != null && genericObj.getType() == "object")
            {
                Serializable.Object obj = (Serializable.Object)genericObj;
                Serializable.Object campusObj = null;
                if (obj.getObject("*") != null)
                {
                    campusObj = obj.getObject("*");
                }
                else if (obj.getObject(this.m_name) != null)
                {
                    campusObj = obj.getObject(this.m_name);
                }
                if (campusObj != null)
                {
                    foreach (var i in campusObj.getChildren())
                    {
                        if (i.Item2.getType() == "object")
                        {
                            Building building = this.getBuilding(i.Item1);
                            if (building != null)
                            {
                                building.apply((Serializable.Object)i.Item2);
                                building.trim(m_buildings);
                            }
                        }
                        else
                        {
                            string match = i.Item1;
                            string replacement = i.Item2.getValue();
                            if (match == "Name")
                            {
                                m_name = replacement;
                            }
                            base.set(match, new Serializable.String(replacement));
                        }
                    }
                }
            }
            return this;
        }
        public Serializable.Object apply(Campus campus)
        {
            this.m_buildings.apply(campus.m_buildings);
            return this;
        }
        public Serializable.Object trim(Serializable.Array siblings)
        {
            /*
             * This object basically canabalizes all other equivilent siblings
            */
            for (int i = 0; i < siblings.size(); i++)
            {
                Campus currentSibling = (Campus)siblings.get(i);
                if (currentSibling != this && currentSibling.getName() == m_name)
                {
                    this.apply(currentSibling);
                    siblings.removeAt(i--);
                }
            }
            return this;
        }
    }

    public class Building : Serializable.Object
    {
        private Serializable.Array m_rooms = new Serializable.Array();
        private string m_name;
        private string m_acronym;
        private int m_id;

        public Building(string fullName, string acronym, int id)
        {
            base.set("Name", new Serializable.String(fullName));
            base.set("Acronym", new Serializable.String(acronym));
            base.set("ID", new Serializable.Number(id));
            base.set("Rooms", m_rooms);
            this.m_name = fullName;
            this.m_acronym = acronym;
            this.m_id = id;
        }
        public string getName()
        {
            return m_name;
        }
        public string getAcronym()
        {
            return m_acronym;
        }
        public int getID()
        {
            return m_id;
        }
        public Room searchRoom(string name)
        {
            var b = m_rooms.getChildren();
            foreach (var i in b)
            {
                Room building = (Room)i.Item2;
                if (m_name.Contains(name) || m_acronym.Contains(name))
                {
                    return building;
                }
            }
            return null;
        }
        public Room getRoom(string name)
        {
            var b = m_rooms.getChildren();
            foreach (var i in b)
            {
                Room building = (Room)i.Item2;
                if (m_name == name || m_acronym == name)
                {
                    return building;
                }
            }
            return null;
        }
        public Room getRoom(int id)
        {
            var b = m_rooms.getChildren();
            foreach (var i in b)
            {
                Room building = (Room)i.Item2;
                if (building.getID() == id)
                {
                    return building;
                }
            }
            return null;
        }
        public Building addRoom(Room room)
        {
            m_rooms.add(room);
            return this;
        }
        public Building addRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                m_rooms.add(room);
            }
            return this;
        }
        public List<Room> getRooms()
        {
            List<Room> rooms = new List<Room>();
            for (int i = 0; i != m_rooms.size(); i++)
            {
                rooms.Add((Room) m_rooms.get(i));
            }
            return rooms;
        }
        public override Object apply(Object genericObj)
        {
            if (genericObj != null && genericObj.getType() == "object")
            {
                Serializable.Object obj = (Serializable.Object)genericObj;
                Serializable.Object buildingObj = null;
                if (obj.getObject("*") != null)
                {
                    buildingObj = obj.getObject("*");
                }
                else if (obj.getObject(this.m_name) != null)
                {
                    buildingObj = obj.getObject(this.m_name);
                }
                else if (obj.getObject(this.m_acronym) != null)
                {
                    buildingObj = obj.getObject(this.m_acronym);
                }
                else if (obj.getObject(this.m_id.ToString()) != null)
                {
                    buildingObj = obj.getObject(this.m_id.ToString());
                }
                if (buildingObj != null)
                {
                    foreach (var i in buildingObj.getChildren())
                    {
                        if (i.Item2.getType() == "object")
                        {
                            Room room = this.getRoom(i.Item1);
                            if (room != null)
                            {
                                room.apply(buildingObj);
                                room.trim(m_rooms);
                            }
                        }
                        else
                        {
                            if (i.Item1 == "Name")
                            {
                                m_name = i.Item2.getValue();
                            }
                            else if (i.Item1 == "Acronym")
                            {
                                m_acronym = i.Item2.getValue();
                            }
                            else if (i.Item1 == "ID")
                            {
                                m_id = (int)((Serializable.Number)i.Item2).get();
                            }
                            base.set(i.Item1, new Serializable.String(i.Item2.getValue()));
                        }
                    }
                }
            }
            return this;
        }
        public Serializable.Object apply(Building building)
        {
            this.m_rooms.apply(building.m_rooms);
            return this;
        }
        public Serializable.Object trim(Serializable.Array siblings)
        {
            /*
             * This object basically canabalizes all other equivilent siblings
            */
            for (int i = 0; i < siblings.size(); i++)
            {
                Building currentSibling = (Building)siblings.get(i);
                if (
                    currentSibling != this &&
                    currentSibling.getName() == m_name &&
                    currentSibling.getAcronym() == m_acronym &&
                    currentSibling.getID() == m_id
                )
                {
                    this.apply(currentSibling);
                    siblings.removeAt(i--);
                }
            }
            return this;
        }
    }
    
    public class Room : Serializable.Object
    {
        private int m_roomNumber;
        private string m_name;
        private int m_id;

        public Room(int roomNumber, string name, int id)
        {
            base.set("RoomNumber", new Serializable.Number(roomNumber));
            base.set("Name", new Serializable.String(name));
            base.set("ID", new Serializable.Number(id));
            this.m_roomNumber = roomNumber;
            this.m_name = name;
            this.m_id = id;
        }
        public int getRoomNumber()
        {
            return this.m_roomNumber;
        }
        public string getName()
        {
            return m_name;
        }
        public int getID() {
            return this.m_id;
        }
        public override Object apply(Object genericObj)
        {
            if (genericObj != null && genericObj.getType() == "object")
            {
                Serializable.Object obj = (Serializable.Object)genericObj;
                Serializable.Object roomObj = null;
                if (obj.getObject("*") != null)
                {
                    roomObj = obj.getObject("*");
                }
                else if (obj.getObject(this.m_name) != null)
                {
                    roomObj = obj.getObject(this.m_name);
                }
                else if (obj.getObject(this.m_id.ToString()) != null)
                {
                    roomObj = obj.getObject(this.m_id.ToString());
                }
                else if (obj.getObject(this.m_roomNumber.ToString()) != null)
                {
                    roomObj = obj.getObject(this.m_roomNumber.ToString());
                }
                if (roomObj != null)
                {
                    foreach (var i in roomObj.getChildren())
                    {
                        if (i.Item1 == "Name")
                        {
                            m_name = i.Item2.getValue();
                        }
                        else if (i.Item1 == "RoomNumber")
                        {
                            m_roomNumber = (int)((Serializable.Number)i.Item2).get();
                        }
                        else if (i.Item1 == "ID")
                        {
                            m_id = (int)((Serializable.Number)i.Item2).get();
                        }
                        base.set(i.Item1, new Serializable.String(i.Item2.getValue()));
                    }
                }
            }
            return this;
        }
        public Serializable.Object trim(Serializable.Array siblings)
        {
            /*
             * This object basically canabalizes all other equivilent siblings
            */
            for (int i = 0; i < siblings.size(); i++)
            {
                Room currentSibling = (Room)siblings.get(i);
                if (
                    currentSibling != this &&
                    currentSibling.getName() == m_name &&
                    currentSibling.getID() == m_id &&
                    currentSibling.getRoomNumber() == m_roomNumber
                )
                {
                    siblings.removeAt(i--);
                }
            }
            return this;
        }
    }
}
