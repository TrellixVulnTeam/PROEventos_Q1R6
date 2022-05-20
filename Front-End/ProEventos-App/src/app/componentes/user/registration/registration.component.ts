import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '@app/Models/identity/User';
import { AccountService } from '@app/services/account.service';
import { ToastrService } from 'ngx-toastr';
import { ValidatorField } from 'src/app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  user = {} as User;
  form!: FormGroup;

  constructor(public fb: FormBuilder, private accountService: AccountService, private router: Router, private toaster: ToastrService) { }

  get f():any {return this.form.controls;}

  ngOnInit() {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmePassword')
    };

    this.form = this.fb.group({
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['',
        [Validators.required, Validators.email]
      ],
      userName: ['', Validators.required],
      password: ['',
        [Validators.required, Validators.minLength(4)]
      ],
      confirmePassword: ['', Validators.required],
    }, formOptions);
  }

register(): void {
  this.user = { ...this.form.value };
  this.accountService.register(this.user).subscribe(
    () => this.router.navigateByUrl('/dashboard'),
    (error: any) => this.toaster.error(error.error)
  )
}

}
