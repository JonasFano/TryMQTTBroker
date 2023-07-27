using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

/* Defines two classes within the Software_measure.Model namespace.
 * points_3D_List --> Represents a collection of points_3D instances
 * points_3D --> Represents a 3D point in the software
 * 
 * These two classes provide a structure to store and manipulate 3D points within the software
 */

namespace Software_measure.Model
{
    public class points_3D_List : IList<points_3D>
    {
        public void Main(string[] args)
        {
            return;
        }

        public List<points_3D> Elements;

        public points_3D_List()
        {
            Elements = new List<points_3D>();
        }

        // Indexer that allows accessing Elements by index
        public points_3D this[int index]
        {
            get => Elements[index];
            set => Elements[index] = value;
        }

        // Number of Elements in the list
        public int Count => Elements.Count;

        // Whether the list is read-only
        public bool IsReadOnly => false;

        // Adds an element to the list
        public void Add(points_3D item)
        {
            Elements.Add(item);
        }

        // Clears all Elements from the list
        public void Clear()
        {
            Elements.Clear();
        }

        // Checks if the list contains a specific element
        public bool Contains(points_3D item)
        {
            return Elements.Contains(item);
        }

        // Copies the Elements of the list to an array, starting at a particular index
        public void CopyTo(points_3D[] array, int arrayIndex)
        {
            Elements.CopyTo(array, arrayIndex);
        }

        // Returns an enumerator that iterates through the list
        public IEnumerator<points_3D> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        // Returns the index of a specific element in the list
        public int IndexOf(points_3D item)
        {
            return Elements.IndexOf(item);
        }

        // Inserts an element into the list at the specified index
        public void Insert(int index, points_3D item)
        {
            Elements.Insert(index, item);
        }

        // Removes the first occurrence of a specific element from the list
        public bool Remove(points_3D item)
        {
            return Elements.Remove(item);
        }

        // Removes the element at the specified index
        public void RemoveAt(int index)
        {
            Elements.RemoveAt(index);
        }

        // Returns an enumerator that iterates through the list
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        // Finds the first element that matches the specified condition
        public points_3D Find(Predicate<points_3D> match)
        {
            var ret = Elements.Find(match);

            if (ret is not null)
            {
                return ret;
            }
            return new points_3D();
        }

        // Clears all Elements from the list
        public void ClearAll()
        {
            Elements.Clear();
        }
    }

    // Implements the INotifyPropertyChanged interface for property change notifications
    public class points_3D : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Has properties: Name, X, Y, Z representing the name and coordinates of the 3D point.
        // Properties are implemented with a getter and setter,
        // which triggers the PropertyChanged event to notify subscribers of property changes. 
        private string s_name = "";
        public string Name
        {
            get { return s_name; }
            set
            {
                s_name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private double value1;
        public double X
        {
            get { return value1; }
            set
            {
                value1 = value;
                NotifyPropertyChanged(nameof(X));
            }
        }

        private double value2;
        public double Y
        {
            get { return value2; }
            set
            {
                value2 = value;
                NotifyPropertyChanged(nameof(Y));
            }
        }

        private double value3;
        public double Z
        {
            get { return value3; }
            set
            {
                value3 = value;
                NotifyPropertyChanged(nameof(Z));
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
