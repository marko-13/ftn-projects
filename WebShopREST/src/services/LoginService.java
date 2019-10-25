package services;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import javax.annotation.PostConstruct;
import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import beans.User;
import dao.UserDAO;

@Path("")
public class LoginService {
	
	@Context
	ServletContext ctx;
	
	public LoginService() {
		
	}
	
	@PostConstruct
	// ctx polje je null u konstruktoru, mora se pozvati nakon konstruktora (@PostConstruct anotacija)
	public void init() {
		// Ovaj objekat se instancira vi�e puta u toku rada aplikacije
		// Inicijalizacija treba da se obavi samo jednom
		if (ctx.getAttribute("userDAO") == null) {
	    	String contextPath = ctx.getRealPath("");
			ctx.setAttribute("userDAO", new UserDAO(contextPath));
		}
	}
	
	@POST
	@Path("/login")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response login(User user, @Context HttpServletRequest request) {
		UserDAO userDao = (UserDAO) ctx.getAttribute("userDAO");
		
		User loggedUser = userDao.find(user.getUsername(), user.getPassword());
		System.out.println(user.getUsername());
		
		if (loggedUser == null) {
			return Response.status(400).entity("Invalid username and/or password").build();
		}
		
		request.getSession().setAttribute("user", loggedUser);
		return Response.status(200).build();
	}
	
	
	@POST
	@Path("/logout")
	//@Consumes(MediaType.APPLICATION_JSON)
	//@Produces(MediaType.APPLICATION_JSON)
	public void logout(@Context HttpServletRequest request) {
		request.getSession().invalidate();
	}
	
	@GET
	@Path("/currentUser")
	//@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public User login(@Context HttpServletRequest request) {
		return (User) request.getSession().getAttribute("user");
	}
	
	
	@GET
	@Path("/getUsers")
	@Produces(MediaType.APPLICATION_JSON)
	public Collection<User> getUsers(){
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		
		return uDAO.findAll();
	}
	
	@POST
	@Path("/changeRole/{uName}/{uRole}")
	public void changeRole(@PathParam("uName") String uName,@PathParam("uRole") int uRole,@Context HttpServletRequest request) {
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> pomocnaMapa = uDAO.getUsers();
		
		User u = pomocnaMapa.get(uName);
		u.setUserRole(uRole);
		
		if(uRole==0) {
			u.setRole("Kupac");
		}
		else if(uRole==1) {
			u.setRole("Administrator");
		}
		else if(uRole==2) {
			u.setRole("Prodavac");
		}
		
		pomocnaMapa.replace(uName, u);
				
		uDAO.saveFileUserChanged(pomocnaMapa, ctx.getRealPath(""));
		
	}
	
	@POST
	@Path("/resetWarning/{uuName}")
	public void resetW(@PathParam("uuName") String uuName) {
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> pomocnaMapa = uDAO.getUsers();
		
		User u = pomocnaMapa.get(uuName);
		
		u.setReportSeller(0);
		
		pomocnaMapa.replace(uuName, u);
		
		uDAO.saveFileUserChanged(pomocnaMapa, ctx.getRealPath(""));
		
	}
	
	
	@POST
	@Path("/addToFav/{idOglasa}")
	public void addToFav(@PathParam("idOglasa") String idOglasa, @Context HttpServletRequest request) {
		
		User u = (User) request.getSession().getAttribute("user");
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		
		HashMap<String, User> pomocnaMapa = uDAO.getUsers();
		
		
		ArrayList<UUID> pomocniUUID = new ArrayList<>();
		if(u.getAdvertisementsFavouritesBuyer()!=null) {
		pomocniUUID = u.getAdvertisementsFavouritesBuyer();
		}
		pomocniUUID.add(UUID.fromString(idOglasa));
		
		u.setAdvertisementsFavouritesBuyer(pomocniUUID);
		
		pomocnaMapa.replace(u.getUsername(), u);
		
		uDAO.saveFileUserChanged(pomocnaMapa, ctx.getRealPath(""));
		
		return;
	}
	
	@POST
	@Path("/removeFromFav/{idOglasa}")
	public void removeFromFav(@PathParam("idOglasa") String idOglasa, @Context HttpServletRequest request) {
		
		User u = (User) request.getSession().getAttribute("user");
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		
		HashMap<String, User> pomocnaMapa = uDAO.getUsers();
		
		ArrayList<UUID> pomocniUUID = u.getAdvertisementsFavouritesBuyer();
		
		for(int i=0; i<pomocniUUID.size(); i++) {
			//System.out.println("MOJ ISPIS  "+pomocniUUID.get(i));
			if(pomocniUUID.get(i).equals(UUID.fromString(idOglasa))) {
				System.out.println("UDJEM OVDE DA OBRISEM OGLAS");
				pomocniUUID.remove(i);
			}
		}

		
		u.setAdvertisementsFavouritesBuyer(pomocniUUID);
		
		pomocnaMapa.replace(u.getUsername(), u);
		
		uDAO.saveFileUserChanged(pomocnaMapa, ctx.getRealPath(""));
		
		return;
	}
	
	
	
}
