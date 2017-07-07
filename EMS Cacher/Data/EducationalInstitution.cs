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
            string name = props.getString("Name");
            string acronym = props.getString("Acronym");
            string department = props.getString("Department");
            base.set("Name", new Serializable.String(name));
            base.set("Acronym", new Serializable.String(acronym));
            base.set("Department", new Serializable.String(department));
            base.set("Campuses", m_campuses);
            this.m_name = name;
            this.m_acronym = acronym;
            this.m_department = department;
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
                if (building.getDescription().Contains(name))
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
                if (building.getDescription() == name)
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
    }
    
    public class Room : Serializable.Object
    {
        private int m_roomNumber;
        private string m_description;
        private int m_id;

        public Room(int roomNumber, string description, int id)
        {
            base.set("RoomNumber", new Serializable.Number(roomNumber));
            base.set("Description", new Serializable.String(description));
            base.set("ID", new Serializable.Number(id));
            this.m_roomNumber = roomNumber;
            this.m_description = description;
            this.m_id = id;
        }
        public int getRoomNumber()
        {
            return this.m_roomNumber;
        }
        public string getDescription()
        {
            return m_description;
        }
        public int getID() {
            return this.m_id;
        }
    }
}
