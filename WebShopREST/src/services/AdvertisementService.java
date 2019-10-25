package services;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.UUID;

import javax.annotation.PostConstruct;
import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import beans.Advertisement;
import beans.Message;
import beans.User;
import dao.AdvertisementDAO;
import dao.MessageDAO;
import dao.UserDAO;

@Path("/ads")
public class AdvertisementService {

	@Context
	ServletContext ctx;
	
	public AdvertisementService() {
	}
	
	@PostConstruct
	public void init() {
		if(ctx.getAttribute("advertisementDAO")==null) {
			String contextPath= ctx.getRealPath("");
			ctx.setAttribute("advertisementDAO", new AdvertisementDAO(contextPath));
			
			System.out.println("DESI SE");
		}
		
		
	}
	
	
	
	@GET
	@Path("/getAds")
	@Produces(MediaType.APPLICATION_JSON)
	public Collection<Advertisement> getAds(){
		
		AdvertisementDAO adsDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		
		return adsDAO.findAll();
	}
	
	@POST
	@Path("/postAd")
	@Produces(MediaType.APPLICATION_JSON)
	public Response postNewAd(Advertisement a, @Context HttpServletRequest request) {
		
		//moramo dodati korisniku tu reklamu koju je napravio
		User u = (User) request.getSession().getAttribute("user");
		
		AdvertisementDAO adsDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		
		ArrayList<UUID> pomocnaListaAds = new ArrayList<>();
		if(u.getAdvertisementsPostedSeller()!=null) {
			pomocnaListaAds = u.getAdvertisementsPostedSeller();
		}
		pomocnaListaAds.add(a.getId());
		

		//Advertisement mojOglas= new Advertisement(a.getName(),a.getPrice(),a.getDescription(),
			//			a.getNumLikes(),a.getNumDislikes(),a.getImgPath(),a.getDatePublished(),
				//		a.getDateExpired(),a.isActive(),a.getRecensions(),a.getCity());
		
		u.setAdvertisementsPostedSeller(pomocnaListaAds);
		
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		//SVE KORISNIKE STAVI U MAPU
		HashMap<String, User> korisnici = uDAO.getUsers();
		
		//korisniku je promenjeno polje postovanih oglasa pa mora da se zameni
		korisnici.replace(u.getUsername(), u);
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		/*UserDAO uxDAO = new UserDAO();
		for(User ux : korisnici.values()) {
			uxDAO.add(ux, ctx.getRealPath(""));
		}
		
		ctx.setAttribute("userDAO", uxDAO);*/
		
		//sad jos samo treba update za listu oglasa
		adsDAO.add(a, ctx.getRealPath(""));
		ctx.setAttribute("advertisementDAO", adsDAO);
		
		return Response.status(200).build();
		
		
	}
	
	
	@POST
	@Path("/editAd/{id}/{ime}/{cena}/{opis}/{grad}")
	public void promeniOglas(@PathParam("id") String id,
			@PathParam("ime") String ime,
			@PathParam("cena") double cena,
			@PathParam("opis") String opis,
			@PathParam("grad") String grad,String slika1, @Context HttpServletRequest request
			) {
		String slika="";
		if(!slika1.equals("NemaNoveSlike")) {
			slika = slika1.substring(17);
			slika = slika.substring(0,slika.length()-2);
			System.out.println("\n\n\n"+slika);
		}
		else {
			slika="NemaNoveSlike";
		}
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		
		User u = (User) request.getSession().getAttribute("user");
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> korisnici = uDAO.getUsers();
		
		AdvertisementDAO aDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		HashMap<UUID, Advertisement> oglasi = aDAO.getAds();
		
		Advertisement mojOglas = oglasi.get(UUID.fromString(id));
		mojOglas.setName(ime);
		mojOglas.setPrice(cena);
		mojOglas.setDescription(opis);
		mojOglas.setCity(grad);
		if(!slika.equals("NemaNoveSlike")) {
			mojOglas.setImgPath(slika);
		}
		
		oglasi.replace(mojOglas.getId(), mojOglas);
		aDAO.saveFileAdChanged(oglasi, ctx.getRealPath(""));
		
		//ako je admin izmenio salji poruku prodavcu ciji je oglas i kupcu ako ga je porucio
		User mojProdavac=null;
		int izadji=0;
		User mojKupac=null;
		int izadji1=0;
		if(u.getUserRole()==1) {
			//za prodavca
			for(User i : korisnici.values()) {
				if(i.getAdvertisementsPostedSeller()!=null) {
					for(int j=0; j<i.getAdvertisementsPostedSeller().size(); j++) {
						if(i.getAdvertisementsPostedSeller().get(j).equals(UUID.fromString(id))) {
							mojProdavac=i;
							izadji=1;
							break;
						}
					}
					
				}
				if(izadji==1) {
					break;
				}
			}
			
			Message m = new Message(mojProdavac.getUsername(), ime, u.getUsername(), "Izmena oglasa",
					"Postovani, \n Ovom prilikom Vas obavestavamo da je Vas oglas, "+mojOglas.getName() +", izmenio admin",
					System.currentTimeMillis());
			mDAO.add(m, ctx.getRealPath(""));
			//ctx.setAttribute("messageDAO", mDAO);
			
			ArrayList<UUID> porukeProdavac = new ArrayList<>();
			if(mojProdavac.getMessagesSeller()!=null) {
				porukeProdavac=mojProdavac.getMessagesSeller();
			}
			porukeProdavac.add(m.getId());
			mojProdavac.setMessagesSeller(porukeProdavac);
			korisnici.replace(mojProdavac.getUsername(), mojProdavac);
			
			//ako je kupac porucio oglas i njemu poruka o promeni
			if(mojOglas.getStatus()==1 || mojOglas.getStatus()==2) {
				for(User i : korisnici.values()) {
					if(i.getAdvertisementsOrderedBuyer()!=null) {
						for(int j=0; j<i.getAdvertisementsOrderedBuyer().size(); j++) {
							if(i.getAdvertisementsOrderedBuyer().get(j).equals(UUID.fromString(id))) {
								mojKupac=i;
								izadji1=1;
								break;
							}
						}
					}
					if(izadji1==1) {
						break;
					}
				}
				
				Message m1 = new Message(mojKupac.getUsername(), ime, u.getUsername(), "Izmena oglasa",
						"Postovani, \n Ovom prilikom Vas obavestavamo da je oglas, "+mojOglas.getName() +", koji ste porucili izmenio admin",
						System.currentTimeMillis());
				mDAO.add(m1, ctx.getRealPath(""));
				
				ArrayList<UUID> porukeKupac = new ArrayList<>();
				if(mojKupac.getMessagesSeller()!=null) {
					porukeKupac=mojKupac.getMessagesSeller();
				}
				porukeKupac.add(m1.getId());
				mojKupac.setMessagesSeller(porukeKupac);
				korisnici.replace(mojKupac.getUsername(), mojKupac);
				
			}
			
		}
		
		if(u.getUserRole()==2) {
			if(mojOglas.getStatus()==1 || mojOglas.getStatus()==2) {
				for(User i : korisnici.values()) {
					if(i.getAdvertisementsOrderedBuyer()!=null) {
						for(int j=0; j<i.getAdvertisementsOrderedBuyer().size(); j++) {
							if(i.getAdvertisementsOrderedBuyer().get(j).equals(UUID.fromString(id))) {
								mojKupac=i;
								//izadji1=1;
								break;
							}
						}
					}
					//if(izadji1==1) {
					//	break;
					//}
				}
				
				Message m1 = new Message(mojKupac.getUsername(), ime, u.getUsername(), "Izmena oglasa",
						"Postovani, \n Ovom prilikom Vas obavestavamo da je oglas koji ste porucili, "+mojOglas.getName()+ ", izmenio prodavac, "+u.getUsername(),
						System.currentTimeMillis());
				
				mDAO.add(m1, ctx.getRealPath(""));
				ArrayList<UUID> porukeKupac = new ArrayList<>();
				if(mojKupac.getMessagesSeller()!=null) {
					porukeKupac=mojKupac.getMessagesSeller();
				}
				porukeKupac.add(m1.getId());
				mojKupac.setMessagesSeller(porukeKupac);
				korisnici.replace(mojKupac.getUsername(), mojKupac);
				
				
				
			}
			//i adminu poruka za izmene
			Message m2 = new Message("admin", ime, u.getUsername(), "Izmena oglasa",
					"Postovani, \n Ovom prilikom Vas obavestavamo da je oglas, "+mojOglas.getName() +", izmenio prodavac, "+u.getUsername(),
					System.currentTimeMillis());
			mDAO.add(m2, ctx.getRealPath(""));
			
			User adm = korisnici.get("admin");
			ArrayList<UUID> porukeAd = new ArrayList<>();
			if(adm.getMessagesSeller()!=null) {
				porukeAd=adm.getMessagesSeller();
			}
			porukeAd.add(m2.getId());
			adm.setMessagesSeller(porukeAd);
			korisnici.replace(adm.getUsername(), adm);

		}
		
		oglasi.replace(mojOglas.getId(), mojOglas);
		aDAO.saveFileAdChanged(oglasi, ctx.getRealPath(""));
		
		
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));

	}
	
	
	@GET
	@Path("/buy/{id}")
	public void buyAd(@PathParam("id") String id, @Context HttpServletRequest request) {
		
		User u = (User) request.getSession().getAttribute("user");
		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> korisnici = uDAO.getUsers();
		
		ArrayList<UUID> listaPorucenih = new ArrayList<>();
		if(u.getAdvertisementsOrderedBuyer()!=null) {
			listaPorucenih=u.getAdvertisementsOrderedBuyer();
		}
		listaPorucenih.add(UUID.fromString(id));
		u.setAdvertisementsOrderedBuyer(listaPorucenih);
		
		korisnici.replace(u.getUsername(), u);
		
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		AdvertisementDAO aDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		HashMap<UUID, Advertisement> oglasi = aDAO.getAds();
		
		Advertisement a = oglasi.get(UUID.fromString(id));
		
		//OVDE
		a.setActive(true);
		a.setStatus(1);
		
		oglasi.replace(a.getId(), a);
		aDAO.saveFileAdChanged(oglasi, ctx.getRealPath(""));
		
		return;
	}
	
	@POST
	@Path("/markD/{id}")
	public void markDeliveredAd(@PathParam("id") String id, @Context HttpServletRequest request) {
		
		User u = (User) request.getSession().getAttribute("user");

		UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
		HashMap<String, User> korisnici = uDAO.getUsers();
		
		
		
		
		User prodavac = null;
		for(User i : korisnici.values()) {
			if(i.getAdvertisementsPostedSeller()==null) {
				continue;
			}
			ArrayList<UUID> mojaL=i.getAdvertisementsPostedSeller();
			
			for(int q=0; q<mojaL.size(); q++) {
				System.out.println(mojaL.get(q));
				if(mojaL.get(q).equals(UUID.fromString(id))) {
				prodavac=i;
				break;
				}
			}
			
		}
		
		ArrayList<UUID> listaStiglihSeller = new ArrayList<>();
		if(prodavac.getAdvertisementsSentSeller()!=null) {
			listaStiglihSeller=prodavac.getAdvertisementsSentSeller();
		}
		listaStiglihSeller.add(UUID.fromString(id));
		prodavac.setAdvertisementsSentSeller(listaStiglihSeller);
		korisnici.replace(prodavac.getUsername(), prodavac);
		
		
		ArrayList<UUID> listaStiglih = new ArrayList<>();
		if(u.getAdvertisementsDeliveredBuyer()!=null) {
			listaStiglih=u.getAdvertisementsDeliveredBuyer();
		}
		listaStiglih.add(UUID.fromString(id));
		u.setAdvertisementsDeliveredBuyer(listaStiglih);

		korisnici.replace(u.getUsername(), u);
	//uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		
		
		AdvertisementDAO aDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		HashMap<UUID, Advertisement> oglasi = aDAO.getAds();
		
		Advertisement a = oglasi.get(UUID.fromString(id));
		
		a.setStatus(2);
		
		oglasi.replace(a.getId(), a);
		aDAO.saveFileAdChanged(oglasi, ctx.getRealPath(""));
		
		Message m = new Message(prodavac.getUsername(), a.getName(), u.getUsername(), "Obavestenje: Dostavljen proizvod",
				"Postovani, \n Ovom prilikom Vas obavestavamo da je vas proizvod uspesno dostavljen kupcu", System.currentTimeMillis());
		
		ArrayList<UUID> porukeProdavac = new ArrayList<>();
		if(prodavac.getMessagesSeller()!=null) {
			porukeProdavac=prodavac.getMessagesSeller();
		}
		porukeProdavac.add(m.getId());
		prodavac.setMessagesSeller(porukeProdavac);
		korisnici.replace(prodavac.getUsername(), prodavac);
		uDAO.saveFileUserChanged(korisnici, ctx.getRealPath(""));
		
		MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
		mDAO.add(m, ctx.getRealPath(""));

		
	}
	
	@GET
	@Path("/delete/{id}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response deleteAd(@PathParam("id") String id, @Context HttpServletRequest request) {
		
		User user = (User)request.getSession().getAttribute("user");

		if(user == null) {
		return Response.status(400).build();
		}
		
		
		AdvertisementDAO adsDAO = (AdvertisementDAO) ctx.getAttribute("advertisementDAO");
		
		HashMap<UUID, Advertisement>pomocniOglasi = adsDAO.getAds();
		
		Advertisement a = pomocniOglasi.get(UUID.fromString(id));
		//int bioStatus = a.getStatus();
		a.setActive(false);
		a.setStatus(-1);
		
		pomocniOglasi.replace(UUID.fromString(id), a);
		
		adsDAO.saveFileAdChanged(pomocniOglasi, ctx.getRealPath(""));
		
		
		//ako je prodavac obrisao oglas adminu stize poruka
		if(user.getUserRole()==2) {
			UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
			HashMap<String, User> mapaKorisnika = uDAO.getUsers();
			
			MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
			HashMap<UUID, Message> poruke = mDAO.getMsgs();
			
			for(User u : mapaKorisnika.values()) {
				//ako je admin salji mu poruku
				if(u.getUserRole()==1) {
					Message m = new Message(u.getUsername(), a.getName(), user.getUsername(), 
							"Obavestenje o brisanju oglasa", "Postovani, \n Ovom prilikom Vas obavestavamo da je"
									+ " prodavac obrisao svoj oglas, "+a.getName()+".", System.currentTimeMillis());
					poruke.put(m.getId(), m);
					
					ArrayList<UUID> msgsUser = new ArrayList<>();
					if(u.getMessagesSeller()!=null) {
						msgsUser=u.getMessagesSeller();
					}
					msgsUser.add(m.getId());
					u.setMessagesSeller(msgsUser);
					uDAO.saveFileUserChanged(mapaKorisnika, ctx.getRealPath(""));
					
				}
			}
			mDAO.saveFileChanged(poruke, ctx.getRealPath(""));
		}
		
		
		//ako je admin obrisao oglas treba poslati poruku prodavcu
		User prodavacCijiJeOglas = null;
		if(user.getUserRole()==1) {
			UserDAO uDAO = (UserDAO) ctx.getAttribute("userDAO");
			HashMap<String, User> mapaKorisnika = uDAO.getUsers();
			
			for(User u : mapaKorisnika.values()) {
				ArrayList<UUID> listaOglasa = new ArrayList<>();
				if(u.getAdvertisementsPostedSeller()!=null) {
					listaOglasa = u.getAdvertisementsPostedSeller();
					if(listaOglasa.contains(UUID.fromString(id))) {
						prodavacCijiJeOglas=u;
						break;
					}
				}
			}
			//System.out.println("VREMEEEE: "+System.currentTimeMillis());
			MessageDAO mDAO = (MessageDAO) ctx.getAttribute("messageDAO");
			Message m = new Message(prodavacCijiJeOglas.getUsername(), a.getName(), user.getUsername(),
					"Obavestenje o brisanju oglasa", "Postovani, \n Ovom prilikom Vas obavestavamo da je Vas oglas, "
							+a.getName()+ ", obrisao admin.", System.currentTimeMillis());
			mDAO.add(m, ctx.getRealPath(""));
			
			//mora i kupcu poruka IPAK NE JER NE MOZE DA SE OBRISE PORUCEN OGLAS
			/*if(bioStatus==1 || bioStatus==2) {
				User kupacKojiJePorucio = null;
				for(User u: mapaKorisnika.values()) {
					ArrayList<UUID> listaPorucenihOglasa = u.getAdvertisementsOrderedBuyer();
					if(listaPorucenihOglasa.contains(UUID.fromString(id))) {
						kupacKojiJePorucio = u;
						break;
					}
					
				}
					Message m1 = new Message(kupacKojiJePorucio.getUsername(), a.getName(), user.getUsername(),
							"Obavestenje o brisanju oglasa", "Postovani, \n Ovom prilikom Vas obavestavamo da je oglas koji ste porucili, "
									+a.getName()+ ", obrisan", System.currentTimeMillis());
					mDAO.add(m1, ctx.getRealPath(""));
				
			}*/
			ArrayList<UUID> pomoc = new ArrayList<>();
			if(prodavacCijiJeOglas.getMessagesSeller()!=null) {
				pomoc=prodavacCijiJeOglas.getMessagesSeller();
			}
			pomoc.add(m.getId());
			
			prodavacCijiJeOglas.setAdvertisementsSentSeller(pomoc);
			mapaKorisnika.replace(prodavacCijiJeOglas.getUsername(), prodavacCijiJeOglas);
			uDAO.saveFileUserChanged(mapaKorisnika, ctx.getRealPath(""));
			
		}
		
		
		return Response.status(200).build();
	}
}
