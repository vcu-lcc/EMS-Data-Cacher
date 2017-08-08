using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using EducationalInstitution;
using Templates;

namespace EMS_Cacher
{
    class AliasHandler
    {
        private List<Tuple<List<Condition>, List<Templates.Action>>> m_aliases = null;

        public AliasHandler(Serializable.Array aliases)
        {
            m_aliases = new List<Tuple<List<Condition>, List<Templates.Action>>>();
            foreach (var alias in aliases.getChildren())
            {
                List<Condition> currConditions = new List<Condition>();
                List<Templates.Action> currActions = new List<Templates.Action>();
                Serializable.Array conditions = alias.Item2.toObject().getArray("Conditions");  // Size == 0?
                Serializable.Array actions = alias.Item2.toObject().getArray("Actions");        // Size == 0?
                for (int i = 0; i != conditions.size(); i++)
                {
                    currConditions.Add(new Condition(conditions.get(i).toObject()));
                }
                for (int i = 0; i != actions.size(); i++)
                {
                    currActions.Add(new Templates.Action(actions.get(i).toObject()));
                }
                m_aliases.Add(new Tuple<List<Condition>, List<Templates.Action>>(currConditions, currActions));
            }
        }

        private void applyTransformation(University university, Campus campus, Building building, Room room)
        {
            foreach (var alias in m_aliases)
            {
                bool allowed = alias.Item1.Count == 0;
                foreach (var condition in alias.Item1)
                {
                    allowed = condition.test(university, campus, building, room);
                    if (allowed)
                    {
                        break;
                    }
                }
                if (allowed)
                {
                    foreach (var action in alias.Item2)
                    {
                        action.act(university, campus, building, room);
                    }
                }
            }
        }

        private void regroup(Serializable.Array parentChildren)
        {
            /*
             * Merge children with equal primitive values.
             */
            List<Tuple<string, Serializable.DataType>> siblings = parentChildren.getChildren();
            for (int i = 0; i != siblings.Count; i++)
            {
                for (int ii = i + 1; ii != siblings.Count; ii++)
                {
                    if (siblings[i].Item2.toObject().equal(siblings[ii].Item2, false))
                    {
                        siblings[i].Item2.toObject().apply(siblings[ii].Item2.toObject());
                        siblings.RemoveAt(ii--);
                    }
                }
            }
        }

        public void applyTransformations(Serializable.Array universities)
        {
            foreach (var _university in universities.getChildren())
            {
                University university = _university.Item2 as University;
                if (university != null)
                {
                    foreach (Campus campus in university.getCampuses())
                    {
                        if (campus != null)
                        {
                            foreach (Building building in campus.getBuildings())
                            {
                                if (building != null)
                                {
                                    foreach (Room room in building.getRooms())
                                    {
                                        if (room != null)
                                        {
                                            applyTransformation(university, campus, building, room);
                                        }
                                    }
                                    regroup(building.getArray("Rooms"));
                                }
                            }
                            regroup(campus.getArray("Buildings"));
                        }
                    }
                    regroup(university.getArray("Campuses"));
                }
            }
            regroup(universities);
        }
    }
}
