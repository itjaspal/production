import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-print-tag',
  templateUrl: './print-tag.component.html',
  styleUrls: ['./print-tag.component.scss']
})
export class PrintTagComponent implements OnInit {

  constructor(
    private _router: Router

  ) { }

  ngOnInit() {
  }

  close() {
    window.history.back();
  }

  print() {
    let head = document.head;
    let style = document.createElement('style');
    style.type = 'text/css';
    style.media = 'print';

    style.appendChild(document.createTextNode('@page { size: A4 landscape; margin: 4mm 0;}'));

    head.appendChild(style);

    window.print();
  }

  setDefaultPrint() {

    this._router.navigateByUrl('/app/defprinter');

  }

}

