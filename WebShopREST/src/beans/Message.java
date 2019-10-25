package beans;

import java.io.Serializable;
import java.util.UUID;

public class Message implements Serializable{

	private UUID id;
	private String receiver;
	private String adName;
	private String sender;
	private String msgTitle;
	private String msgContent;
	private long dateAndTime;
	private boolean read;
	private boolean deleted;
	
	public Message(){
		
	}

	public Message(String receiver,String adName, String sender, String msgTitle, String msgContent, long dateAndTime) {
		super();
		this.id=UUID.randomUUID();
		this.receiver=receiver;
		this.adName = adName;
		this.sender = sender;
		this.msgTitle = msgTitle;
		this.msgContent = msgContent;
		this.dateAndTime = dateAndTime;
		this.read=false;
		this.deleted=false;
	}

	//*************************
	//GETTERI I SETTERI
	//*************************
	public UUID getId() {
		return id;
	}
	
	public void setId(UUID id) {
		this.id=id;
	}
	
	public String getReceiver() {
		return receiver;
	}
	
	public void setReceiver(String receiver) {
		this.receiver=receiver;
	}
	
	public String getAdName() {
		return adName;
	}

	public void setAdName(String adName) {
		this.adName = adName;
	}

	public String getSender() {
		return sender;
	}

	public void setSender(String sender) {
		this.sender = sender;
	}

	public String getMsgTitle() {
		return msgTitle;
	}

	public void setMsgTitle(String msgTitle) {
		this.msgTitle = msgTitle;
	}

	public String getMsgContent() {
		return msgContent;
	}

	public void setMsgContent(String msgContent) {
		this.msgContent = msgContent;
	}

	public long getDateAndTime() {
		return dateAndTime;
	}

	public void setDateAndTime(long dateAndTime) {
		this.dateAndTime = dateAndTime;
	}
	
	public boolean getRead() {
		return read;
	}
	
	public void setRead(boolean read) {
		this.read=read;
	}
	
	public boolean getDeleted() {
		return deleted;
	}
	
	public void setDeleted(boolean deleted) {
		this.deleted=deleted;
	}
	//************************************
	//GOTOVI GETTERI I SETTERI
	//***********************************

	@Override
	public String toString() {
		return "Message [adName=" + adName + ", sender=" + sender + ", msgTitle=" + msgTitle + ", msgContent="
				+ msgContent + ", dateAndTime=" + dateAndTime + "]";
	}
	
	private static final long serialVersionUID = -6346983813831198493L;
	
}
