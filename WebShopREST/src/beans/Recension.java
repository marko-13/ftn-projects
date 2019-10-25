package beans;

import java.io.Serializable;
import java.util.UUID;

public class Recension implements Serializable{

	private UUID id;
	private String ad;
	private String recAuthor;
	private String title;
	private String content;
	private String imgPath;
	private boolean adDescriptionCorrect;
	private boolean dealFulfilled;
	
	public Recension() {
		
	}

	public Recension(String ad, String recAuthor, String title, String content, String imgPath,
			boolean isAdDescriptionCorrect, boolean isDealFulfilled) {
		super();
		this.id=UUID.randomUUID();
		this.ad = ad;
		this.recAuthor = recAuthor;
		this.title = title;
		this.content = content;
		this.imgPath = imgPath;
		this.adDescriptionCorrect = isAdDescriptionCorrect;
		this.dealFulfilled = isDealFulfilled;
	}

	//****************************
	//GETTERI I SETTERI
	//****************************
	public UUID getId() {
		return id;
	}
	
	public void setId(UUID id) {
		this.id=id;
	}
	
	public String getAd() {
		return ad;
	}

	public void setAd(String ad) {
		this.ad = ad;
	}

	public String getRecAuthor() {
		return recAuthor;
	}

	public void setRecAuthor(String recAuthor) {
		this.recAuthor = recAuthor;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public String getImgPath() {
		return imgPath;
	}

	public void setImgPath(String imgPath) {
		this.imgPath = imgPath;
	}

	public boolean isAdDescriptionCorrect() {
		return adDescriptionCorrect;
	}

	public void setAdDescriptionCorrect(boolean isAdDescriptionCorrect) {
		this.adDescriptionCorrect = isAdDescriptionCorrect;
	}

	public boolean isDealFulfilled() {
		return dealFulfilled;
	}

	public void setDealFulfilled(boolean isDealFulfilled) {
		this.dealFulfilled = isDealFulfilled;
	}
	//********************************
	//GOTOVI GETTERI I SETTERI
	//********************************

	@Override
	public String toString() {
		return "Recension [ad=" + ad + ", recAuthor=" + recAuthor + ", title=" + title + ", content=" + content
				+ ", imgPath=" + imgPath + ", isAdDescriptionCorrect=" + adDescriptionCorrect + ", isDealFulfilled="
				+ dealFulfilled + "]";
	}
	
	private static final long serialVersionUID = -2266963231822530722L;
	
}
