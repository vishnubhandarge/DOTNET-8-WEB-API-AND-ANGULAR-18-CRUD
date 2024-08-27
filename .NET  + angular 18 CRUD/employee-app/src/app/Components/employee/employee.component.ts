import {
  Component,
  ElementRef,
  inject,
  OnInit,
  ViewChild,
  viewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Employee } from '../../Models/employee';
import { EmployeeService } from '../../Services/employee.service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css',
})
export class EmployeeComponent implements OnInit {
  @ViewChild('myModal') model: ElementRef | undefined;

  employeeForm: FormGroup = new FormGroup({});

  employeeList: Employee[] = [];
  empService = inject(EmployeeService);
  getEmployees() {
    this.empService.getAllEmployees().subscribe((res) => {
      this.employeeList = res;
    });
  }

  ngOnInit(): void {
    this.setFormState();
    this.getEmployees();
  }
  constructor(private fb: FormBuilder) {}
  openModal() {
    const empModal = document.getElementById('myModal');
    if (empModal != null) {
      empModal.style.display = 'block';
    }
  }

  closeModal() {
    this.setFormState();
    if (this.model != null) {
      this.model.nativeElement.style.display = 'none';
    }
  }

  setFormState() {
    this.employeeForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required]],
      email: ['', Validators.required],
      mobile: ['', [Validators.required]],
      age: ['', [Validators.required]],
      salary: ['', [Validators.required]],
      status: [false, [Validators.required]],
    });
  }

  formValue: any;

  onSubmit() {
    console.log(this.employeeForm.value);
    if(this.employeeForm.invalid){
        alert('all fields required');
        return; 
      }
      this.formValue = this.employeeForm.value;
      this.empService.addEmployee(this.formValue).subscribe((res) => {
        alert('employee added');
        this.getEmployees();
        this.employeeForm.reset();
        this.closeModal();
      });
  }

  onDelete(id: number){
    const inConfirm = confirm("Sure to delete");
    if(inConfirm == true){
      this.empService.deleteEmployee(id).subscribe((res) =>{
      alert('employee deleted');
      this.getEmployees();
    });
    }
  }

  onEdit(employee: Employee){
    this.openModal();
    this.employeeForm.patchValue(employee);
  }
}
