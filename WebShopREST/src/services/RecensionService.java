package services;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
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

import beans.Advertisement;
import beans.Message;
import beans.Recension;
import beans.User;
import dao.AdvertisementDAO;
import dao.MessageDAO;
import dao.RecensionDAO;
import dao.UserDAO;

@Path("rec")
public class RecensionService {

	@Context
	ServletContext ctx;
	
	public RecensionService() {
		
	}
	
	@PostConstruct
	public void init() {
		if(ctx.getAttribute("recensionDAO")==null) {
			String contextPath = ctx.getRealPath("");
			ctx.setAttribute("recensionDAO", new RecensionDAO(contextPath));
		}
	}
	
	//@GET
	//@Path("/nesto")
	//public void nesto() {
		
		//return;
	//}
	
	@GET
	@Path("/getRecs")
	@Produces(MediaType.APPLICATION_JSON)
	public Collection<Recension> getRecs(){
		RecensionDAO rDAO = (RecensionDAO) ctx.getAttribute("recensionDAO");
		
		return rDAO.findAll();
	}
	
	
	@POST
	@Path("/deleteRec/{id}")
	public void brisiRec(@PathParam("id") String id) {
		RecensionDAO rDAO = (RecensionDAO) ctx.getAttribute("recensionDAO");
		
		HashMap<UUID, Recension> recenzije = rDAO.getRecs();
		
		Recension r = recenzije.get(UUID.fromString(id));
		r.setTitle("obrisanaRecenzija");
		
		recenzije.replace(r.getId(), r);
		rDAO.saveFileChanged(recenzije, ctx.getRealPath(""));
	}
	
	
	@POST
	@Consumes(MediaType.APPLICATION_JSON)
	@Path("/editRec/{naslov}/{content}/{opisOk}/{dogovorOk}/{idRec}")
	public void editRec(@PathParam("naslov") String naslov,
			@PathParam("content") String content,
			@PathParam("opisOk") boolean opisOk,
			@PathParam("dogovorOk") boolean dogovorOk,
			@PathParam("idRec") String idRec,String slika1, @Context HttpServletRequest request
			) {
		
		RecensionDAO rDAO = (RecensionDAO) ctx.getAttribute("recensionDAO");
		HashMap<UUID, Recension> recenzije = rDAO.getRecs();
		Recension mojaR = recenzije.get(UUID.fromString(idRec));
		
		System.out.println("\n\n\n"+slika1);
		
		String slika="NemaSliku";
		if(!slika1.equals("NemaSliku")) {
		slika = slika1.substring(13);
		slika = slika.substring(0,slika.length()-2);
		System.out.println("\n\n\n"+slika);
		}

		
		mojaR.setTitle(naslov);
		mojaR.setContent(content);
		mojaR.setImgPath(slika);
		mojaR.setAdDescriptionCorrect(opisOk);
		mojaR.setDealFulfilled(dogovorOk);
		
		recenzije.replace(mojaR.getId(), mojaR);
		rDAO.saveFileChanged(recenzije, ctx.getRealPath(""));
		
		//posalji poruku kupcu ciji je oglas
		User ulog = (User) request.getSession().getAttribute("user");

		//treba oglas da bi nasao korisnika ciji je to oglas
		AdvertisementDAO aDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		HashMap<UUID, Advertisement> oglasi = aDAO.getAds();
		
		Advertisement mojOgl = null;
		for(Advertisement i : oglasi.values()) {
			if(i.getRecensions()!=null) {
				List<UUID> recenzijeOglasa = i.getRecensions();
				for(int q=0; q<recenzijeOglasa.size(); q++) {
					if(recenzijeOglasa.get(q).equals(UUID.fromString(idRec))) {
						mojOgl = i;
						break;
					}
				}
			}
		}
		
		if(mojOgl==null) {
			System.out.println("NIJE NASAO OGLAS KOD EDITOVANJA RECENZIJE");
			return;
		}
		
		User postavljacOgl = null;
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> korisnici = uDAO.getUsers();
		
		for(User kor : korisnici.values()) {
			//sent jer je kraci niz a review moze samo na oglase koji su dostavljeni
			if(kor.getAdvertisementsSentSeller()!=null) {
				ArrayList<UUID> dostavljeniOglasi = kor.getAdvertisementsSentSeller();
				for(int k=0; k<dostavljeniOglasi.size(); k++) {
					if(dostavljeniOglasi.get(k).equals(mojOgl.getId())) {
						postavljacOgl=kor;
						break;
					}
				}
			}
		}
		
		if(postavljacOgl==null) {
			System.out.println("NIJE NASAO PRODAVCA KOJI JE POSTAVIO TAJ OGLAS KOD EDITOVANJA RECENZIJE");
			return;
		}
		
		Message m = new Message(postavljacOgl.getUsername(), mojOgl.getName(), "admin",
				"Izmena recenzije", "Postovani, \n Ovom prilikom Vas obavestavamo da je "+ ulog.getUsername()+
				" izmenio postavljenu za Vas oglas", System.currentTimeMillis());
		
		ArrayList<UUID> porukePrimljene = new ArrayList<>();
		if(postavljacOgl.getMessagesSeller()!=null) {
			porukePrimljene=postavljacOgl.getMessagesSeller();
		}
		porukePrimljene.add(m.getId());
		postavljacOgl.setMessagesSeller(porukePrimljene);
		
		korisnici.replace(postavljacOgl.getUsername(), postavljacOgl);
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		mDAO.add(m, ctx.getRealPath(""));
		
	}
	
	@POST
	@Path("/addRec/{pom1}/{pom2}/{idOgl}/{prijaviProdavca}")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public void dodajRec(@PathParam("pom1") int pom1, @PathParam("pom2") int pom2,@PathParam("idOgl") String idOgl,
			@PathParam("prijaviProdavca") boolean prijaviProdavca,
			Recension r,
			@Context HttpServletRequest request) {
		
		
		
		
		System.out.println("\n\nEVO ME\n\n");
		
		//ulogovani korisnik
		User ulog = (User) request.getSession().getAttribute("user");
		
		RecensionDAO rDAO = (RecensionDAO) ctx.getAttribute("recensionDAO");
		HashMap<UUID, Recension> mapaRecenzija = rDAO.getRecs();
		
		mapaRecenzija.put(r.getId(), r);
		rDAO.add(r, ctx.getRealPath(""));
		
		
		//dodeli like dislike proizvodu ako treba
		AdvertisementDAO aDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		HashMap<UUID, Advertisement> mapaOglasa = aDAO.getAds();
		Advertisement a = mapaOglasa.get(UUID.fromString(idOgl));
		int broj=-1000;
		
		if(pom2==1) {
			broj=a.getNumLikes();
			broj++;
			a.setNumLikes(broj);
		}
		else if(pom2==-1) {
			broj=a.getNumDislikes();
			broj++;
			a.setNumDislikes(broj);
		}
		
		
		
		//dodaj recenziju u listu recenzija oglasa
		List<UUID> listaRecIzOgl = new ArrayList<>();
		if(a.getRecensions()!=null) {
			listaRecIzOgl = a.getRecensions();
		}
		listaRecIzOgl.add(r.getId());
		
		a.setRecensions(listaRecIzOgl);
		mapaOglasa.replace(a.getId(), a);
		aDAO.saveFileAdChanged(mapaOglasa, ctx.getRealPath(""));
		
		
		
		
		//dodeli like dislke prodavcu ako treba
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> mapaKorisnika = uDAO.getUsers();
		User u=null;
		for(User i : mapaKorisnika.values()) {
			ArrayList<UUID> mojNiz = new ArrayList<>();
			if(i.getAdvertisementsPostedSeller()!=null) {
				mojNiz = i.getAdvertisementsPostedSeller();
			}
			for(int j=0; j<mojNiz.size(); j++) {
				if(mojNiz.get(j).equals(UUID.fromString(idOgl))) {
					u=i;
					break;
				}
			}
			if(u!=null) {
				break;
			}
		}
		Message tempM = null;
		if(prijaviProdavca) {
			int brPrijava = u.getReportSeller();
			brPrijava++;
			u.setReportSeller(brPrijava);
			
			Message me = new Message(u.getUsername(), r.getAd(), "admin","Warning: "+r.getTitle(),
					"Postovani, \n Prijavljeni ste od strane korisnika i sada imate "+brPrijava+" opomena"
							+ ". Iskljucenje naloga je sa 3 opomene", System.currentTimeMillis());
			tempM=me;
		}
		
		//napravi poruku i posalji je prodavcu za recenziju koja je ostavljenja
		Message m = new Message( u.getUsername(), r.getAd(), ulog.getUsername(), 
			"Rec: "+r.getTitle(), r.getContent(), System.currentTimeMillis());
		
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		
		
		mDAO.add(m, ctx.getRealPath(""));
		
		ArrayList<UUID> porukeProdavac = u.getMessagesSeller();
		
		if(tempM!=null) {
			mDAO.add(tempM, ctx.getRealPath(""));
			porukeProdavac.add(tempM.getId());
		}
		
		porukeProdavac.add(m.getId());
		
		u.setMessagesSeller(porukeProdavac);
		
		double broj1=-1000;
		if(pom1==1) {
			broj1=u.getLikesNumberSeller();
			broj1++;
			u.setLikesNumberSeller(broj1);;
		}
		else if(pom1==-1) {
			broj1=u.getDislikesNumberSeller();
			broj1++;
			u.setDislikesNumberSeller(broj1);
		}
		
		
			mapaKorisnika.replace(u.getUsername(), u);
			uDAO.saveFileUserChanged(mapaKorisnika, ctx.getRealPath(""));
		
		
		
		return;
	}
	
}
