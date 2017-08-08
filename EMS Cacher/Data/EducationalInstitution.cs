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

        public University(Serializable.Object props)
        {
            base.apply(props);
            base.set("Campuses", m_campuses);
            base.set("Name", props.getString("Name"));
            base.set("Acronym", props.getString("Acronym"));
            base.set("Department", props.getString("Department"));
        }
        public University()
        {
            base.set("Campuses", m_campuses);
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
            return base.getString("Name");
        }
        public string getAcronym()
        {
            return base.getString("Acronym");
        }
        public string getDepartment()
        {
            return base.getString("Department");
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

        public Campus(string name)
        {
            base.set("Name", new Serializable.String(name));
            base.set("Buildings", m_buildings);
        }
        public Campus(): this("")
        {
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
            return base.getString("Name");
        }
        public string getAcronym()
        {
            return base.getString("Acronym");
        }
        public int getID()
        {
            return (int)base.getNumber("ID");
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
        public Building(string fullName, string acronym, int id)
        {
            base.set("Name", new Serializable.String(fullName));
            base.set("Acronym", new Serializable.String(acronym));
            base.set("ID", new Serializable.Number(id));
            base.set("Rooms", new Serializable.Array());
        }
        public Building() : this("", "", -1)
        {
        }
        public string getName()
        {
            return base.getString("Name");
        }
        public string getAcronym()
        {
            return base.getString("Acronym");
        }
        public int getID()
        {
            return (int)base.getNumber("ID");
        }
        public Room searchRoom(string name)
        {
            var b = base.getArray("Rooms").getChildren();
            foreach (var i in b)
            {
                Room building = (Room)i.Item2;
                if (base.getString("Name").Contains(name) || base.getString("Acronym").Contains(name))
                {
                    return building;
                }
            }
            return null;
        }
        public Room getRoom(string name)
        {
            var b = base.getArray("Rooms").getChildren();
            foreach (var i in b)
            {
                Room building = (Room)i.Item2;
                if (base.getString("Name") == name || base.getString("Acronym") == name)
                {
                    return building;
                }
            }
            return null;
        }
        public Room getRoom(int id)
        {
            var b = base.getArray("Rooms").getChildren();
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
            base.getArray("Rooms").add(room);
            return this;
        }
        public Building addRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                base.getArray("Rooms").add(room);
            }
            return this;
        }
        public List<Room> getRooms()
        {
            List<Room> rooms = new List<Room>();
            for (int i = 0; i != base.getArray("Rooms").size(); i++)
            {
                rooms.Add(base.getArray("Rooms").get(i) as Room);
            }
            return rooms;
        }
    }
    
    public class Room : Serializable.Object
    {
        public Room(int roomNumber, string name, int id)
        {
            base.set("RoomNumber", new Serializable.Number(roomNumber));
            base.set("Name", new Serializable.String(name));
            base.set("ID", new Serializable.Number(id));
        }
        public Room() : this(-1, "", -1)
        {
        }
        public int getRoomNumber()
        {
            return (int)base.getNumber("ID");
        }
        public string getName()
        {
            return base.getString("Name");
        }
        public int getID() {
            return (int)base.getNumber("RoomNumber");
        }
    }
}
