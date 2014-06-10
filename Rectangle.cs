/// <summary>
/// A rectangle structure.
/// </summary>
public struct Rectangle {
	
	// initializer
	
	/// <summary>
	/// Initializes a new rectangle struct.
	/// </summary>
	/// <param name='cx'>the center point on x-axis</param>
	/// <param name='cy'>the center point on y-axis</param>
	/// <param name='radius'>the rectangle radius</param>
	public Rectangle(float cx, float cy, float r){
		
		CenterX = cx;
		CenterY = cy;
		
		Radius = r;
		
		X = cx - r;
		Y = cy - r;
		X2 = cx + r;
		Y2 = cy + r;
	}
	
	
	// properties
	
	/// <summary>
	/// Gets or sets the center of rectangle on x-axis.
	/// </summary>
	/// <value>the x value</value>
	public float CenterX { get; private set; }
	
	/// <summary>
	/// Gets or sets the center of rectangle on y-axis.
	/// </summary>
	/// <value>the y value</value>
	public float CenterY { get; private set; }
	
	/// <summary>
	/// Gets or sets the radius of the rectangle.
	/// </summary>
	/// <value>the value for the radius</value>
	public float Radius { get; private set; }
	
	/// <summary>
	/// Gets or sets the x coordinate for left corner.
	/// </summary>
	/// <value>the x value</value>
	public float X { get; private set; }
	
	/// <summary>
	/// Gets or sets the x coordinate for right corner.
	/// </summary>
	/// <value>the x value</value>
	public float X2 { get; private set; }
	
	/// <summary>
	/// Gets or sets the y coordinate for top corner.
	/// </summary>
	/// <value>the y value</value>
	public float Y { get; private set; }
	
	/// <summary>
	/// Gets or sets the y coordinate for bottom corner.
	/// </summary>
	/// <value>the y value</value>
	public float Y2 { get; private set; }
	
	
	// methods
	
	/// <summary>
	/// Checks whether the rectangle contains another rectangle.
	/// </summary>
	/// <param name='rect'>the other rectangle</param>
	/// <returns>true if contained, otherwise false</returns>
	public bool Contains(Rectangle rect){
		
		return rect.X >= X && rect.Y >= Y && 
				rect.X2 <= X2 && rect.Y2 <= Y2;
	}
	
	/// <summary>
	/// Checks whether the rectangle intersectses with another rectangle.
	/// </summary>
	/// <param name='rect'>the other rectangle</param>
	/// <returns>true if intersects, otherwise false</returns>
	public bool IntersectsWith(Rectangle rect){
		
		return !(rect.X >= X2 || rect.X2 <= X || rect.Y >= Y2 || rect.Y2 <= Y);
	}
	
	/// <summary>
	/// Returns the coordinates of the rectangle in format x/y | x2/y2. 
	/// </summary>
	/// <returns>a <see cref="System.String"/> that represents the current <see cref="Rectangle"/></returns>
	public string ToString(){
		
		return X + "/" + Y + " | " + X2 + "/" + Y2;
	}
}