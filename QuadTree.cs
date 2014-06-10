using System.Collections.Generic;

/// <summary>
/// Interface that expects a rectangle property.
/// </summary>
public interface IHasRect {
	
	/// <summary>
	/// Gets the rectangle.
	/// </summary>
	Rectangle Rectangle { get; }
}

/// <summary>
/// Quad tree.
/// </summary>
/// <typeparam name="T">the generic type T that guarantees the data object has a rectangle</typeparam>
public class QuadTree<T> where T : IHasRect {
	
	
	// attributes
	
	/// <summary>The child nodes of this tree.</summary>
	private QuadTree<T>[] _children;
	
	/// <summary>The stored_data items.</summary>
	private List<T> _data;
	
	/// <summary>The bounding_box of this tree.</summary>
	private readonly Rectangle _rect;
	
	/// <summary>The size of the_smallest area.</summary>
	private readonly float _smallestAreaSize;
	
	
	// initializer
  
	/// <summary>
	/// Initializes a new instance of the QuadTree class.
	/// </summary>
	/// <param name='bounds'>the bounding area</param>
	/// <param name='capacity'>the data capacity of a tree node</param>
	/// <param name='smallestSize'>the smallest area size</param>
	public QuadTree(Rectangle rect, int capacity, float smallestAreaSize){
		
		_rect = rect;
		_smallestAreaSize = smallestAreaSize;
		
		_data = new List<T>(capacity);
		_children = null;
	}
	
	
	// properties
	
	/// <summary>
	/// Gets the total number of all subnodes and all nodes in this node.
	/// </summary>
	public int Count {
		
		get {
			int count = 0;
			
			if(null != _children)
				foreach(QuadTree<T> child in _children)
					count += child.Count;
						
			return (count +=  _data.Count);
		}
	}
	
	/// <summary>
	/// Gets the bounding area of tree.
	/// </summary>
	public Rectangle Rectangle { get { return _rect; } }
	
	
	// methods
	
	/// <summary>
	/// Insert a specified item.
	/// </summary>
	/// <param name='item'>the item</param>
	/// <returns>true if successful, otherwise false</returns>
	public void Insert(T item){
	
		// ignore objects that do not belong in this quad tree
		if(!_rect.Contains(item.Rectangle))
			return;
		
		// otherwise, subdivide if necessary and then add the item to whichever node will accept it
		if(null == _children)
			Subdivide();
		
		foreach(QuadTree<T> child in _children)
			if(child.Rectangle.Contains(item.Rectangle)){
				child.Insert(item);
				return;
			}
		
		// if none of the subnodes completely contained the item or we are at the smallest subnode size 
		// allowed >> add the item as node data
		_data.Add(item);
	}
	
	/// <summary>
	/// Create four children that divide this quad into four quads of equal area.
	/// </summary>
	public void Subdivide(){
	
		// the smallest subnode has one area
		if(!(_smallestAreaSize > 4 * _rect.Radius * _rect.Radius)){
	
			_children = new QuadTree<T>[4];
			
			float halfRadius = 0.5f * _rect.Radius;
			
			_children[0] = new QuadTree<T>(new Rectangle(_rect.CenterX - halfRadius, _rect.CenterY - halfRadius, halfRadius), _data.Capacity, _smallestAreaSize);
			_children[1] = new QuadTree<T>(new Rectangle(_rect.CenterX + halfRadius, _rect.CenterY - halfRadius, halfRadius), _data.Capacity, _smallestAreaSize);
			_children[2] = new QuadTree<T>(new Rectangle(_rect.CenterX - halfRadius, _rect.CenterY + halfRadius, halfRadius), _data.Capacity, _smallestAreaSize);
			_children[3] = new QuadTree<T>(new Rectangle(_rect.CenterX + halfRadius, _rect.CenterY + halfRadius, halfRadius), _data.Capacity, _smallestAreaSize);
		}
	}
	
	/// <summary>
	/// Queries a range to find all item that appear within it.
	/// </summary>
	/// <param name='range'>the search range</param>
	/// <returns>a list of all found items</returns>
	public List<T> QueryRange(Rectangle range){
		
		List<T> results = new List<T>();
		
		// abort if the range does not intersect this quad
		if(_rect.IntersectsWith(range)){
		
			// check objects at this quad level
			foreach(T item in _data)
				if(range.Contains(item.Rectangle))
					results.Add(item);
			
			// stop if there are no children
			if(null != _children)
			
				// otherwise, add the items from the children
				foreach(QuadTree<T> child in _children)
					results.AddRange(child.QueryRange(range));
		}
		
		return results;
	}
}