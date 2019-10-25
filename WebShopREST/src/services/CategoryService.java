package services;

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

import beans.Category;
import dao.CategoryDAO;

@Path("category")
public class CategoryService {

	
	@Context
	ServletContext ctx;
	
	public CategoryService() {
		
	}
	
	@PostConstruct
	public void init() {
		if(ctx.getAttribute("categoryDAO")==null) {
			String contextPath = ctx.getRealPath("");
			ctx.setAttribute("categoryDAO", new CategoryDAO(contextPath));
		}
	}
	
	
	@GET
	@Path("/getCats")
	@Produces(MediaType.APPLICATION_JSON)
	public Collection<Category> getAds(){
		
		CategoryDAO catDAO = (CategoryDAO) ctx.getAttribute("categoryDAO");
		
		return catDAO.findAll();
	}
	
	@POST
	@Path("/addCat")
	@Consumes(MediaType.APPLICATION_JSON)
	public Response addCat(Category c, @Context HttpServletRequest request) {
		
		CategoryDAO catDAO = (CategoryDAO) ctx.getAttribute("categoryDAO");
		
		HashMap<UUID, Category> pomocni = catDAO.getCategories();
		
		pomocni.put(c.getId(), c);
		
		catDAO.saveFileCategoryChanged(pomocni, ctx.getRealPath(""));
		
		return Response.status(200).build();
		
	}
	
	@POST
	@Path("/editCat/{id}")
	public void editC(@PathParam("id") String id, Category c) {
	
		
		CategoryDAO catDAO = (CategoryDAO) ctx.getAttribute("categoryDAO");
		
		HashMap<UUID, Category> pomocni = catDAO.getCategories();
		
		Category mojaC = pomocni.get(UUID.fromString(id));
		
		mojaC.setActive(false);
		
		pomocni.replace(mojaC.getId(), mojaC);
		pomocni.put(c.getId(), c);
		
		
		catDAO.saveFileCategoryChanged(pomocni, ctx.getRealPath(""));
		
	}
	
	
	@POST
	@Path("/deleteCat/{catId}")
	public void deleteCat(@PathParam("catId") String catId) {
		
		CategoryDAO catDAO = (CategoryDAO) ctx.getAttribute("categoryDAO");
		
		HashMap<UUID, Category> pomocni = catDAO.getCategories();
		
		
		Category c=pomocni.get(UUID.fromString(catId));
		c.setActive(false);
		
		pomocni.replace(UUID.fromString(catId), c);
		
		catDAO.saveFileCategoryChanged(pomocni, ctx.getRealPath(""));
		
		return;
			
		
	}
	
	
	
}










