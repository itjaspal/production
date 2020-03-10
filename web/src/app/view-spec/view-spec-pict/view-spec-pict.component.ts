import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatDialogRef } from '@angular/material';



@Component({
  selector: 'app-view-spec-pict',
  templateUrl: './view-spec-pict.component.html',
  styleUrls: ['./view-spec-pict.component.scss']
})
export class ViewSpecPictComponent implements OnInit {

  public imageSource: any;
  public actionView : any = "True";

 
  constructor(
    private sanitizer: DomSanitizer,  
    public dialogRef: MatDialogRef<any>,
  ) { }

  ngOnInit() {

    //console.log("My base64 image : " + sessionStorage.getItem('session-image-base64')) 
    this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${sessionStorage.getItem('session-image-base64')}`);
    

  }

  close() { 
    //window.history.back();
    this.dialogRef.close();
  }

} 
