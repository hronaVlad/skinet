import { Component, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { IAddress } from 'src/app/shared/models/address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  address: IAddress;

  constructor(private accountService: AccountService, private toastrServie: ToastrService) { }

  ngOnInit(): void {
  }

  saveAsDefault(): void {
    const address = this.checkoutForm.get('addressForm').value;

    this.accountService.updateAddress(address).subscribe( {
      next: () => {
        this.toastrServie.success('Address saved');
        this.checkoutForm.get('addressForm').reset(address);
      },
      error: err => {
        this.toastrServie.error(err);
        throw err;
      }
    });
  }
}
