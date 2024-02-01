import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form!: FormGroup;

  constructor(private formBuilder:FormBuilder, 
    private http:HttpClient,
    private router:Router
    ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: '',
      email: '',
      password: ''
    })
  }
  submit(): void {
    this.http.post('http://localhost:5000/register', this.form.getRawValue())
    .subscribe(() => {
      this.router.navigate(['/login'])
    })
  }
}
