import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  returnUrl: string;

  constructor(private accountService: AccountService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/shop';

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{3}$')]],
      password: ['', Validators.required],
    });
  }


  onSubmit(): void {
    if (this.loginForm.valid) {
      this.accountService.login(this.loginForm.value).subscribe (
        {
          next: () => {
            console.log("user logged in");

            this.router.navigate([this.returnUrl]);
          },
          error: (err: any) => { },
          complete: () => { }
        }
      );
    }else {
      throw this.loginForm.errors;
    }
  }

}
