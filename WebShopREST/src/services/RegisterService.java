package services;

import javax.annotation.PostConstruct;
import javax.servlet.ServletContext;
import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.Consumes;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import beans.User;
import dao.UserDAO;

@Path("")
public class RegisterService {

	@Context
	ServletContext ctx;
	
	public RegisterService() {
		
	}
	
	@PostConstruct
	//ctx polje je null u konstuktoru, mora se pozvati nakon konstruktora
	public void init() {
		//instancira se vise puta ali inicijalizacija 
		//treba da se izvrsi samo jednom
		if(ctx.getAttribute("userDAO")==null) {
			String contextPath = ctx.getRealPath("");
			ctx.setAttribute("userDAO", new UserDAO(contextPath));
		}
	}

	@POST
	@Path("/register")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response register(User user, @Context HttpServletRequest request) {
		UserDAO userDAO = (UserDAO) ctx.getAttribute("userDAO");
		
		String path = ctx.getRealPath("");
		System.out.println("nesto");
		/*User u = userDAO.find(user.getUsername(), user.getPassword());
		if(u==null) {
			return Response.status(400).build();
		}*/
		if(userDAO.find(user.getUsername())) {
			return Response.status(400).entity("Username already exists").build();
		}
		
		
		userDAO.add(user, path);
		ctx.setAttribute("userDAO", userDAO);
		
		User loggedUser = userDAO.find(user.getUsername(), user.getPassword());
		request.getSession().setAttribute("user", loggedUser);

		
		return Response.status(200).build();
	}
}
