package beans;

import java.io.Serializable;
import java.util.List;

public class Seller extends User implements Serializable{

	private List<Advertisement> postedAds;
	private List<Advertisement> deliveredAds;
	private List<Message> messages;
	private int numLikes;
	private int numDislikes;
	
	public Seller() {
		
	}

	public Seller(List<Advertisement> postedAds, List<Advertisement> deliveredAds, List<Message> messages, int numLikes,
			int numDislikes) {
		super();
		this.postedAds = postedAds;
		this.deliveredAds = deliveredAds;
		this.messages = messages;
		this.numLikes = numLikes;
		this.numDislikes = numDislikes;
		
		this.setRole("Seller");
		this.setUserRole(2);
	}

	//***************************************
	//GETTERI I SETTERI
	//***************************************
	public List<Advertisement> getPostedAds() {
		return postedAds;
	}

	public void setPostedAds(List<Advertisement> postedAds) {
		this.postedAds = postedAds;
	}

	public List<Advertisement> getDeliveredAds() {
		return deliveredAds;
	}

	public void setDeliveredAds(List<Advertisement> deliveredAds) {
		this.deliveredAds = deliveredAds;
	}

	public List<Message> getMessages() {
		return messages;
	}

	public void setMessages(List<Message> messages) {
		this.messages = messages;
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
	//*******************************************
	//GOTOVI GETTERI I SETTERI
	//*******************************************

	@Override
	public String toString() {
		return "Seller [postedAds=" + postedAds + ", deliveredAds=" + deliveredAds + ", messages=" + messages
				+ ", numLikes=" + numLikes + ", numDislikes=" + numDislikes + "]";
	}
	
	private static final long serialVersionUID = 3129300366096038641L;

}
