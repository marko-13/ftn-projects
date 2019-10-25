package dao;

import java.io.File;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;

import beans.Recension;

public class RecensionDAO {

	private HashMap<UUID, Recension> recensions = new HashMap<>();
	
	public RecensionDAO() {
		
	}
	
	//****************||||||||||*******
	public RecensionDAO(String contextPath) {
		loadRecs(contextPath);
	}
	
	public Recension find(UUID id) {
		if(recensions.containsKey(id)) {
			return recensions.get(id);
		}
		
		return null;
	}
	
	public HashMap<UUID, Recension> getRecs(){
		return recensions;
	}
	
	public void setRecs(HashMap<UUID, Recension> recs) {
		this.recensions=recs;
	}
	
	public Collection<Recension> findAll(){
		return recensions.values();
	}
	
	//********************************************
	public void add(Recension r, String contextPath) {
		try {
			File file=new File(contextPath+"/recs.json");
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			ArrayList<Recension> proba = new ArrayList<>();
			
			Recension[] postojeceRec = objectMapper.readValue(file, Recension[].class);
			
			for(Recension i: postojeceRec) {
				proba.add(i);
			}
			proba.add(r);
			
			objectMapper.writeValue(new File(contextPath+"/recs.json"), proba);
			recensions.put(r.getId(), r);
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	public void loadRecs(String contextPath) {
		try {
			File file= new File(contextPath+"/recs.json");
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			Recension[] recenzije = null;
			
			if(file.exists() && file.length()!=0) {
				recenzije=objectMapper.readValue(file, Recension[].class);
				for(Recension i: recenzije) {
					recensions.put(i.getId(), i);
				}
			}
			else {
				file.createNewFile();
				ArrayList<Recension> proba = new ArrayList<>();

				proba.add(new Recension("Neki oglas","Neki autor","Neki naslov","Neki opis","Neka slika",true,true));
				
				objectMapper.writeValue(new File(contextPath+"/recs.json"), proba);
				
				recenzije=objectMapper.readValue(file, Recension[].class);
				
				for(Recension i: recenzije) {
					recensions.put(i.getId(), i);
				}
			}
		}
		catch(Exception ex){
			System.out.println(ex);
			ex.printStackTrace();
		}
	}
	
	
	public void saveFileChanged(HashMap<UUID, Recension>recs, String contextPath) {
		try {
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT, true);
			
			ArrayList<Recension> proba = new ArrayList<>();
			
			for(Recension i: recs.values()) {
				proba.add(i);
			}
			
			objectMapper.writeValue(new File(contextPath+"/recs.json"), proba);
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
}
