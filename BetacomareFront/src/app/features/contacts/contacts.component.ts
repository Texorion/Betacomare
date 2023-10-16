import { Component } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent {
  success: boolean = false;
  message1: string = "Thank you for contacting us!";
  message2: string = "You will never get our answer.";

  constructor(){}

  messageSuccess(){
    this.success = true;
  }
}
