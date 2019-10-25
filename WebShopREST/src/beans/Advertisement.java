package beans;

import java.io.Serializable;
import java.util.List;
import java.util.UUID;

public class Advertisement implements Serializable{
	
	private int status; //0=active -1=deleted 1=u realizaciji 2=dostavljen
	private UUID id;
	private String name;
	private double price;
	private String description;
	private int numLikes;
	private int numDislikes;
	private String imgPath;
	private long datePublished;
	private long dateExpired;  //active je bio boolean ali je promenjen u int jer status moze biti 4 vrednsoti
	private boolean active; // ZAPRAVO JE OVO STATUS 0=active -1=deleted 1=u realizaciji 2=dostavljen
	private List<UUID> recensions;
	private String city;
	
	public Advertisement() {
		
	}
	
	public Advertisement(String name, double price, String description, int numLikes, int numDislikes, String imgPath,
			long datePublished, long dateExpired, boolean active, List<UUID> recensions, String city) {
		super();
		this.status=0;
		this.id=UUID.randomUUID();
		this.name = name;
		this.price = price;
		this.description = description;
		this.numLikes = numLikes;
		this.numDislikes = numDislikes;
		this.imgPath = imgPath;
		this.datePublished = datePublished;
		this.dateExpired = dateExpired;
		this.active = active;
		this.recensions = recensions;
		this.city = city;
	}

	//************************
	//GETTERI I SETTERI
	//************************
	public UUID getId() {
		return id;
	}
	
	public void setId(UUID id) {
		this.id=id;
	}
	
	public int getStatus() {
		return status;
	}
	
	public void setStatus(int status) {
		this.status=status;
	}
	
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public double getPrice() {
		return price;
	}

	public void setPrice(double price) {
		this.price = price;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public int getNumLikes() {
		return numLikes;
	}

	public void setNumLikes(int numLikes) {
		this.numLikes = numLikes;
	}

	public int getNumDislikes() {
		return numDislikes;
	}

	public void setNumDislikes(int numDislikes) {
		this.numDislikes = numDislikes;
	}

	public String getImgPath() {
		return imgPath;
	}

	public void setImgPath(String imgPath) {
		this.imgPath = imgPath;
	}

	public long getDatePublished() {
		return datePublished;
	}

	public void setDatePublished(long datePublished) {
		this.datePublished = datePublished;
	}

	public long getDateExpired() {
		return dateExpired;
	}

	public void setDateExpired(long dateExpired) {
		this.dateExpired = dateExpired;
	}

	public boolean isActive() {
		return active;
	}

	public void setActive(boolean active) {
		this.active = active;
	}

	public List<UUID> getRecensions() {
		return recensions;
	}

	public void setRecensions(List<UUID> recensions) {
		this.recensions = recensions;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}
	//*********************************
	//GOTOVI GETTERI I SETTERI
	//*********************************

	@Override
	public String toString() {
		return "Advertisement [name=" + name + ", price=" + price + ", description=" + description + ", numLikes="
				+ numLikes + ", numDislikes=" + numDislikes + ", imgPath=" + imgPath + ", datePublished="
				+ datePublished + ", dateExpired=" + dateExpired + ", active=" + active + ", city=" + city + "]";
	}
	
	
	private static final long serialVersionUID = 663439730841001089L;
	
}
