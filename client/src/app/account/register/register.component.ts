import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { map, of, switchMap, timer } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {

    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      email: [null, [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{3}$')],
                    [this.validateEmail()]
             ],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    this.accountService.register(this.registerForm.value).subscribe ( {
      next: () => {
        console.log("user registered");
        this.router.navigate(['/shop']);
      }
    });
  }

  validateEmail() : AsyncValidatorFn {
    return (control: AbstractControl) => {
      return timer(500).pipe(
        switchMap( () => {
           const email = control.value;

           if (!email) {
             return of(null);
           }

          return this.accountService.checkEmailExists(email).pipe (
            map( res => {
              return res ? {emailInUse: true} : null;
            }));

         })
       );
    }
  }

}
