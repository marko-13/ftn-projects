package beans;

import java.io.Serializable;
import java.util.List;

public class Buyer extends User implements Serializable{

	private List<Advertisement> ads;
	private List<Advertisement> orderedAds;
	private List<Advertisement> favouriteAds;
	
	public Buyer(){
		
	}

	public Buyer(List<Advertisement> ads, List<Advertisement> orderedAds, List<Advertisement> favouriteAds) {
		super();
		this.ads = ads;
		this.orderedAds = orderedAds;
		this.favouriteAds = favouriteAds;
		
		this.setRole("Buyer");
		this.setUserRole(0);
	}

	//************************************
	//GETTERI I SETTERI
	//************************************
	public List<Advertisement> getAds() {
		return ads;
	}

	public void setAds(List<Advertisement> ads) {
		this.ads = ads;
	}

	public List<Advertisement> getOrderedAds() {
		return orderedAds;
	}

	public void setOrderedAds(List<Advertisement> orderedAds) {
		this.orderedAds = orderedAds;
	}

	public List<Advertisement> getFavouriteAds() {
		return favouriteAds;
	}

	public void setFavouriteAds(List<Advertisement> favouriteAds) {
		this.favouriteAds = favouriteAds;
	}
	//************************************
	//GOTOVI GETTERI I SETTERI
	//************************************

	@Override
	public String toString() {
		return "Buyer [ads=" + ads + ", orderedAds=" + orderedAds + ", favouriteAds=" + favouriteAds + "]";
	}
	
	private static final long serialVersionUID = 1037596765122684360L;
	
}
