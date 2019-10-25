package beans;

import java.io.Serializable;
import java.util.List;
import java.util.UUID;

public class Category implements Serializable{

	private UUID id;
	private String name;
	private String description;
	private List<Advertisement> advertisements;
	private boolean active;
	
	public Category() {
		
	}
	
	public Category(String name, String description, List<Advertisement> advertisements, boolean active) {
		super();
		this.id=UUID.randomUUID();
		this.name = name;
		this.description = description;
		this.advertisements = advertisements;
		this.active=active;
	}

	//*************************
	//GETTERI I SETTERI
	//*************************
	public UUID getId() {
		return id;
	}
	
	public void setId(UUID id) {
		this.id=id;
	}
	
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public List<Advertisement> getAdvertisements() {
		return advertisements;
	}

	public void setAdvertisements(List<Advertisement> advertisements) {
		this.advertisements = advertisements;
	}
	public boolean getActive() {
		return active;
	}

	public void setActive(boolean active) {
		this.active = active;
	}
	//*************************
	//GOTOVI GETTERI I SETTERI
	//************************
	
	@Override
	public String toString() {
		return "Category [name=" + name + ", description=" + description + ", advertisements=" + advertisements + "]";
	}
	
	private static final long serialVersionUID = 9130567812082527733L;

}
