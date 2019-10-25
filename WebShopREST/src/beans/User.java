package beans;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.UUID;

public class User implements Serializable {
	
	private String username;
	private String password;
	private String firstName;
	private String lastName;
	private String role;
	private String phone;
	private String city;
	private String email;
	private long date; //jquerry ima svoju lib za datepicker //GregoiranCalndar importuj
	private int userRole; //0-kupac, 1-administrator, 2-prodavac
	
	private ArrayList<UUID> advertisementsOrderedBuyer; //ovo UUID je kljuc koji je jedinstven za svaki oglas
	private ArrayList<UUID> advertisementsDeliveredBuyer;
	private ArrayList<UUID> advertisementsFavouritesBuyer;
	
	private ArrayList<UUID> advertisementsPostedSeller;
	private ArrayList<UUID> advertisementsSentSeller; //oni oglasi koji su dostavljeni
	private ArrayList<UUID> messagesSeller; //to je inbox
	
	private double likesNumberSeller;
	private double dislikesNumberSeller;
	
	private int reportSeller;
	
	public User() {
	}

	public User(String username, String password, String firstName,
				String lastName, String role, String phone, String city,
				String email, long date, int userRole) {
		super();
		this.username=username;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
		this.role=role;
		this.phone=phone;
		this.city=city;
		this.email = email;
		this.date=date;
		this.userRole=userRole;
		
		this.advertisementsOrderedBuyer = new ArrayList<>(); //BILO JE =null
		this.advertisementsDeliveredBuyer = new ArrayList<>(); //BILO JE =null
		this.advertisementsFavouritesBuyer = new ArrayList<>(); //BILO JE =null
		this.advertisementsPostedSeller = new ArrayList<>(); //BILO JE =null
		this.advertisementsSentSeller = new ArrayList<>(); //BILO JE =null
		this.messagesSeller = new ArrayList<>(); //BILO JE =null
		this.likesNumberSeller = 0;
		this.dislikesNumberSeller = 0;
		
		this.reportSeller=0;
		//this.setUserRole(1);
		//this.setRole("Administrator");
	}
	

	public User(String username, String password, String firstName, String lastName, String role, String phone,
			String city, String email, long date, int userRole, ArrayList<UUID> advertisementsOrderedBuyer,
			ArrayList<UUID> advertisementsDeliveredBuyer, ArrayList<UUID> advertisementsFavouritesBuyer,
			ArrayList<UUID> advertisementsPostedSeller, ArrayList<UUID> advertisementsSentSeller,
			ArrayList<UUID> messagesSeller, double likesNumberSeller, double dislikesNumberSeller) {
		super();
		this.username = username;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
		this.role = role;
		this.phone = phone;
		this.city = city;
		this.email = email;
		this.date = date;
		this.userRole = userRole;
		this.advertisementsOrderedBuyer = advertisementsOrderedBuyer;
		this.advertisementsDeliveredBuyer = advertisementsDeliveredBuyer;
		this.advertisementsFavouritesBuyer = advertisementsFavouritesBuyer;
		this.advertisementsPostedSeller = advertisementsPostedSeller;
		this.advertisementsSentSeller = advertisementsSentSeller;
		this.messagesSeller = messagesSeller;  //ne samo od sellera nego od svih korisnika
		this.likesNumberSeller = likesNumberSeller;
		this.dislikesNumberSeller = dislikesNumberSeller;
		
		this.reportSeller=0;
	}

	//***************************
	//GETTERI I SETTERI
	//***************************
	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}
	
	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}
	
	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}
	
	public String getLastName() {
		return lastName;
	}

	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	public String getRole() {
		return role;
	}

	public void setRole(String role) {
		this.role = role;
	}

	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}
	
	public long getDate() {
		return date;
	}

	public void setDate(long date) {
		this.date = date;
	}
	
	public int getUserRole() {
		return userRole;
	}

	public void setUserRole(int userRole) {
		this.userRole = userRole;
	}
	
	public ArrayList<UUID> getAdvertisementsOrderedBuyer() {
		return advertisementsOrderedBuyer;
	}

	public void setAdvertisementsOrderedBuyer(ArrayList<UUID> advertisementsOrderedBuyer) {
		this.advertisementsOrderedBuyer = advertisementsOrderedBuyer;
	}

	public ArrayList<UUID> getAdvertisementsDeliveredBuyer() {
		return advertisementsDeliveredBuyer;
	}

	public void setAdvertisementsDeliveredBuyer(ArrayList<UUID> advertisementsDeliveredBuyer) {
		this.advertisementsDeliveredBuyer = advertisementsDeliveredBuyer;
	}

	public ArrayList<UUID> getAdvertisementsFavouritesBuyer() {
		return advertisementsFavouritesBuyer;
	}

	public void setAdvertisementsFavouritesBuyer(ArrayList<UUID> advertisementsFavouritesBuyer) {
		this.advertisementsFavouritesBuyer = advertisementsFavouritesBuyer;
	}

	public ArrayList<UUID> getAdvertisementsPostedSeller() {
		return advertisementsPostedSeller;
	}

	public void setAdvertisementsPostedSeller(ArrayList<UUID> advertisementsPostedSeller) {
		this.advertisementsPostedSeller = advertisementsPostedSeller;
	}

	public ArrayList<UUID> getAdvertisementsSentSeller() {
		return advertisementsSentSeller;
	}

	public void setAdvertisementsSentSeller(ArrayList<UUID> advertisementsSentSeller) {
		this.advertisementsSentSeller = advertisementsSentSeller;
	}

	public ArrayList<UUID> getMessagesSeller() {
		return messagesSeller;
	}

	public void setMessagesSeller(ArrayList<UUID> messagesSeller) {
		this.messagesSeller = messagesSeller;
	}

	public double getLikesNumberSeller() {
		return likesNumberSeller;
	}

	public void setLikesNumberSeller(double likesNumberSeller) {
		this.likesNumberSeller = likesNumberSeller;
	}

	public double getDislikesNumberSeller() {
		return dislikesNumberSeller;
	}

	public void setDislikesNumberSeller(double dislikesNumberSeller) {
		this.dislikesNumberSeller = dislikesNumberSeller;
	}
	
	public int getReportSeller() {
		return reportSeller;
	}
	
	public void setReportSeller(int reportSeller) {
		this.reportSeller=reportSeller;
	}

	//*******************************
	//GOTOVI GETTERI I SETTERI
	//*******************************

	
	@Override
	public String toString() {
		return "User [username=" + username + ", password=" + password + ", firstName=" + firstName + ", lastName="
				+ lastName + ", role=" + role + ", phone=" + phone + ", city=" + city + ", email=" + email + ", date="
				+ date + ", userRole=" + userRole + "]";
	}

	private static final long serialVersionUID = 6640936480584723344L;

}
