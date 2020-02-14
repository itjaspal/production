import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-send-prod-cancel',
  templateUrl: './send-prod-cancel.component.html',
  styleUrls: ['./send-prod-cancel.component.scss']
})
export class SendProdCancelComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  close() {
    window.history.back();
  }

  save() {
    window.history.back();
  }

}
