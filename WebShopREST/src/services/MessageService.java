package services;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import javax.annotation.PostConstruct;
import javax.servlet.ServletContext;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import beans.Message;
import beans.User;
import dao.MessageDAO;
import dao.UserDAO;

@Path("/message")
public class MessageService {

	@Context
	ServletContext ctx;
	
	public MessageService() {
		
	}
	
	@PostConstruct
	public void init() {
		if(ctx.getAttribute("messageDAO")==null) {
			String contextPath = ctx.getRealPath("");
			ctx.setAttribute("messageDAO", new MessageDAO(contextPath));
		}
	}
	
	@GET
	@Path("/load")
	@Produces(MediaType.APPLICATION_JSON)
	public Collection<Message> getMsgs() {
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		
		return mDAO.findAll();
	}
	
	
	@POST
	@Path("/compose")
	@Consumes(MediaType.APPLICATION_JSON)
	public Response composeMessage(Message m) {
		
		//dodaj uuid poruke u korisnikovu listu uuida poruka
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> korisnici = uDAO.getUsers();
		User u=null;
		if(korisnici.containsKey(m.getReceiver())) {
			u = korisnici.get(m.getReceiver());
		}
		else {
			return Response.status(400).build();
		}
		
		ArrayList<UUID> korisnikPoruke = new ArrayList<>();
		if(u.getMessagesSeller()!=null) {
			korisnikPoruke = u.getMessagesSeller();
		}
		
		korisnikPoruke.add(m.getId());
		
		u.setMessagesSeller(korisnikPoruke);
		
		korisnici.replace(u.getUsername(), u);
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		//dodaj poruku u file sa porukama
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");		
		mDAO.add(m, ctx.getRealPath(""));
		
		//DA LI CU OVDE STAVITI ctx.setAttribute
		//******************************************
		
		
		
		return Response.status(200).build();
	}
	
	@POST
	@Path("/edit/{adName}/{naslov}/{opis}/{id}")
	public void editMsg(@PathParam("adName") String adName, @PathParam("naslov") String naslov, @PathParam("opis") String opis,
			@PathParam("id") String id) {
		
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		HashMap<UUID, Message> poruke = mDAO.getMsgs();
		
		Message m = poruke.get(UUID.fromString(id));
		m.setMsgTitle(naslov);
		m.setMsgContent(opis);
		m.setAdName(adName);
		
		poruke.replace(m.getId(), m);
		
		mDAO.saveFileChanged(poruke, ctx.getRealPath(""));
		
	}
	
	@GET
	@Path("/delete/{id}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response deleteMsg(@PathParam("id") String id) {
		
		System.out.println(id);
		
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		HashMap<UUID, Message> mojePoruke = mDAO.getMsgs();
		
		Message m=mojePoruke.get(UUID.fromString(id));
		
		m.setDeleted(true);
		
		mojePoruke.replace(UUID.fromString(id), m);
		
		mDAO.saveFileChanged(mojePoruke, ctx.getRealPath(""));
		
		
		return Response.status(200).build();
	}
	
	
}
