import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  constructor(private spinnerSerivce: NgxSpinnerService) { }

  public loading(): void {
    this.spinnerSerivce.show(
      undefined, {
      type: "", // line-scale
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333',
      size: "medium"
    });
  }

  public idle(): void {
    this.spinnerSerivce.hide();
  }
}
