package dao;

//import java.io.BufferedReader;
import java.io.File;
//import java.io.FileReader;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
//import java.util.Map;
//import java.util.StringTokenizer;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;

import beans.User;

/***
 * <p>Klasa namenjena da ucita korisnike iz fajla i pruža operacije nad njima (poput pretrage).
 * Korisnici se nalaze u fajlu WebContent/users.json u obliku: <br>
 * firstName;lastName;email;username;password</p>
 * <p><b>NAPOMENA:</b> Lozinke se u praksi <b>nikada</b> ne snimaju u cistom tekstualnom obliku.</p>
 * @author Lazar
 *
 */
public class UserDAO {
	private HashMap<String, User> users = new HashMap<>();
	
	
	public UserDAO() {
		
	}
	
	/***
	 * @param contextPath Putanja do aplikacije u Tomcatu. Može se pristupiti samo iz servleta.
	 */
	public UserDAO(String contextPath) {
		//kad pravi UserDao odmah ih i ucita
		loadUsers(contextPath);
	}
	
	/**
	 * Vraca korisnika za prosleðeno korisnicko ime i šifru. Vraca null ako korisnik ne postoji
	 * @param username
	 * @param password
	 * @return
	 */
	public User find(String username, String password) {
		if (!users.containsKey(username)) {
			return null;
		}
		User user = users.get(username);
		if (!user.getPassword().equals(password)) {
			return null;
		}
		return user;
	}
	
	public HashMap<String, User> getUsers() {
		return users;
	}

	public void setUsers(HashMap<String, User> users) {
		this.users = users;
	}
	
	public boolean find(String username) {
		if(!users.containsKey(username))
			return false;
		return true;
	}
	
	public User pretragaKorisnika(String username) {
		if(!users.containsKey(username))
			return null;
		
		User user= users.get(username);
		return user;
	}
	
	public Collection<User> findAll(){
		return users.values();
	}
	
	//*******************************************
	public void add(User u, String contextPath) {
		try {
			File file = new File(contextPath+"/users.json");
			System.out.println(contextPath);
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			ArrayList<User> proba=new ArrayList<>();
			//proba.add(new User("pera","pera","Petar","Petrovic","Administrator","3463","NS","pera@gmail.com","43434",1));
			
			User[] postojeciUsers = objectMapper.readValue(file, User[].class);
			System.out.println("Registered users:"+postojeciUsers);
			
			for(User i : postojeciUsers) {
				proba.add(i);
			}
			proba.add(u);
			
			objectMapper.writeValue(new File(contextPath+"/users.json"), proba);
			users.put(u.getUsername(), u);
			
			System.out.println("Ispis nakon dodavanja korisnika:\n"+users);
		}
		catch(Exception ex){
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	/**
	 * Uèitava korisnike iz WebContent/users.txt fajla i dodaje ih u mapu {@link #users}.
	 * Kljuè je korisnièko ime korisnika.
	 * @param contextPath Putanja do aplikacije u Tomcatu
	 */
	public void loadUsers(String contextPath) {
		try {
			File file = new File(contextPath+"/users.json");
			System.out.println(contextPath);
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			
			User[] korisnici = null;
			
			
			if(file.exists() && file.length()!=0) {
				korisnici = objectMapper.readValue(file, User[].class);
				
				for(User u: korisnici) {
					users.put(u.getUsername(), u);
				}
				System.out.println(users);
			}
			else {
				file.createNewFile();
				ArrayList<User> proba = new ArrayList<>();
				proba.add(new User("admin","admin","ADM","IN","Administrator","555333", "NS", "admin@gmail.com",5533,1));
				proba.add(new User("kupac","kupac","KU","PAC","Kupac","555333", "NS", "kupac@gmail.com",5533,0));
				proba.add(new User("prodavac","prodavac","PRO","DAVAC","Prodavac","555333", "NS", "prodavac@gmail.com",5533,2));

				//********OVDE RUCNO DODAJEM OGLASE KAO DA IH JE POSTAVIO PRODAVAC
				//ArrayList<UUID> mojaLista=new ArrayList<>();
				
				//mojaLista.add(UUID.fromString("d46c2793-ff1c-45ae-9156-00e2ef030662")); //tv
				//mojaLista.add(UUID.fromString("f3885afd-4932-4d8c-94d2-dba711b5df74")); //pegla
				//mojaLista.add(UUID.fromString("fa608528-290f-4a2e-b916-1c254b741559")); //rerna
				//mojaLista.add(UUID.fromString("b22812d7-b0c0-495f-b223-b940b21c7555")); //toster

				
				//proba.get(2).setAdvertisementsPostedSeller(mojaLista);
				//********GOTOVO RUCNO DODAVANJE OGLASA
				
				objectMapper.writeValue(new File(contextPath+"/users.json"), proba);
				
				korisnici = objectMapper.readValue(file, User[].class);
				
				for(User u: korisnici) {
					users.put(u.getUsername(), u);
				}
				System.out.println(users);
			}
			System.out.println("loaded users"+korisnici);
			//if(korisnici==null) {
				//proba.add(new User("peraa","peraa","Petarr","Petrovicc","Administrator","3463","NS","pera@gmail.com","43434",1));
				//proba.add(new User("peraaa","peraaa","Petarrr","Petroviccc","Kupac","3463","NS","pera@gmail.com","43434",0));
				
				//objectMapper.writeValue(new File(contextPath+"/users.json"), proba);
			//}
			
			
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	//sacuvaj u fajl kad se dese neke promene na useru a ne kad se doda
	//da bi se odrzala konzistentnost
	public void saveFileUserChanged(HashMap<String, User> users, String contextPath) {
		try {
			//File file = new File(contextPath+"/users.json");
			System.out.println(contextPath);
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT, true);
			
			ArrayList<User> proba = new ArrayList<>();
			
			for(User u : users.values()) {
				proba.add(u);
			}
			objectMapper.writeValue(new File(contextPath+"/users.json"), proba);
			
			System.out.println(users + "U file upisi");
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
}
