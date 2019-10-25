package dao;

import java.io.File;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;

import beans.Message;

public class MessageDAO {
	
	private HashMap<UUID, Message> messages = new HashMap<>();
	
	public MessageDAO() {
		
	}
	
	//*****************|||||********************
	public MessageDAO(String contextPath) {
		loadMsgs(contextPath);
	}
	
	public Message find(UUID id) {
		if(messages.containsKey(id)) {
			Message m =messages.get(id);
			return m;
		}
		
		return null;
	}
	
	public HashMap<UUID, Message> getMsgs(){
		return messages;
	}
	
	public void setMsgs(HashMap<UUID, Message> msgs) {
		this.messages=msgs;
	}
	
	public Collection<Message> findAll(){
		return messages.values();
	}
	
	//**************************************************************
	public void add(Message m, String contextPath) {
		try {
			File file=new File(contextPath+"/messages.json");
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			ArrayList<Message> proba = new ArrayList<>();
			
			Message[] postojecePoruke = objectMapper.readValue(file, Message[].class);
			
			for(Message i: postojecePoruke) {
				proba.add(i);
			}
			proba.add(m);
			
			objectMapper.writeValue(new File(contextPath+"/messages.json"), proba);
			messages.put(m.getId(), m);
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	public void loadMsgs(String contextPath) {
		try {
			File file=new File(contextPath+"/messages.json");
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			Message[] poruke = null;
			
			if(file.exists() && file.length()!=0) {
				poruke = objectMapper.readValue(file, Message[].class);
				for(Message i: poruke) {
					messages.put(i.getId(), i);
				}
			}
			else {
				file.createNewFile();
				ArrayList<Message> proba = new ArrayList<>();
				
				proba.add(new Message("Neki primalac", "Oglas1", "Korisnik1Poslao", "Naslov1", "Sadrzaj1", 50));
				
				objectMapper.writeValue(new File(contextPath+"/messages.json"), proba);
				
				poruke = objectMapper.readValue(file, Message[].class);
				
				for(Message i: poruke) {
					messages.put(i.getId(), i);
				}
			}
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	public void saveFileChanged(HashMap<UUID, Message> mes, String contextPath) {
		try {
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT, true);
			
			ArrayList<Message> proba = new ArrayList<>();
			
			for(Message i: mes.values()) {
				proba.add(i);
			}
			objectMapper.writeValue(new File(contextPath+"/messages.json"), proba);
			
			
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	
	
	
	
	
	
	

}
