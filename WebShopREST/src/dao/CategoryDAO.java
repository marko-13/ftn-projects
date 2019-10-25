package dao;

import java.io.File;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;

import beans.Category;

public class CategoryDAO {
	private HashMap<UUID, Category> categories = new HashMap<>();
	
	
	public CategoryDAO() {
		
	}
	
	public CategoryDAO(String contextPath) {
		//kad pravi CategoryDAO odmah ih i ucita
		loadCategories(contextPath);
	}
	
	public Category find(UUID name) {
		if(categories.containsKey(name)) {
			Category cat=categories.get(name);
			return cat;
		}
		
		return null;
	}
	
	public HashMap<UUID, Category> getCategories(){
		return categories;
	}
	
	public void setCategories(HashMap<UUID, Category> categories) {
		this.categories=categories;
	}
	
	public Collection<Category> findAll(){
		return categories.values();
	}
	
	//****************************************************
	public void add(Category c, String contextPath) {
		try {
			File file=new File(contextPath+"/categories.json");
			System.out.println("Putanja do kategorija:\n"+contextPath+"\n\n");
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			ArrayList<Category> proba=new ArrayList<>();
			
			Category[] postojeceKategorije = objectMapper.readValue(file, Category[].class);
			System.out.println("Existing categories:\n"+postojeceKategorije+"\n\n");
			
			for(Category i: postojeceKategorije) {
				proba.add(i);
			}
			proba.add(c);
			
			objectMapper.writeValue(new File(contextPath+"/categories.json"), proba);
			categories.put(c.getId(), c);
			
			System.out.println("After adding:\n"+categories+"\n\n");
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	public void loadCategories(String contextPath) {
		try {
			File file=new File(contextPath+"/categories.json");
			System.out.println(contextPath);
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			
			Category[] kategorije = null;
			System.out.println(file);
			
			if(file.exists() && file.length()!=0) {
				kategorije=objectMapper.readValue(file, Category[].class);
				for(Category c : kategorije) {
					categories.put(c.getId(), c);
				}
			}
			else {
				file.createNewFile();
				
				ArrayList<Category> proba = new ArrayList<>();
				proba.add(new Category("Kategorija 1","Ovo je neki opis 1", null,true));
				proba.add(new Category("Kategorija 2","Ovo je neki opis 2", null,true));
				
				objectMapper.writeValue(new File(contextPath+"/categories.json"), proba);
				
				kategorije = objectMapper.readValue(file, Category[].class);
				
				for(Category c : kategorije) {
					categories.put(c.getId(), c);
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
	
	
	public void saveFileCategoryChanged(HashMap<UUID, Category> cats, String contextPath) {
		try {
			//File file = new File(contextPath+"/users.json");
			System.out.println(contextPath);
			
			ObjectMapper objectMapper = new ObjectMapper();
			objectMapper.configure(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT, true);
			objectMapper.configure(DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT, true);
			
			ArrayList<Category> proba = new ArrayList<>();
			
			for(Category c : cats.values()) {
				proba.add(c);
			}
			objectMapper.writeValue(new File(contextPath+"/categories.json"), proba);
			
			System.out.println(cats + "U file upisi");
		}
		catch(Exception ex) {
			System.out.println(ex);
			ex.printStackTrace();
		}
		finally {
			
		}
	}
	
	
	
	
	
	
	
	
	
}
