import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent {
  showCreateForm: boolean = false;
  isUserRoleDisabled: boolean = false;
  message: string = '';
  newStudentForm: FormGroup;

  constructor(private studentService: StudentService, private fb: FormBuilder) {
    this.newStudentForm = this.fb.group({
      studentName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: [''], // Add validations as needed
      mobileNumber: ['', Validators.required],
      password: ['', Validators.required],
      UserRole: [{ value: 'Student', disabled: true }, Validators.required],
    });
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
  }

  createStudent() {
    if (this.newStudentForm.valid) {
      this.studentService.createStudent(this.newStudentForm.value).subscribe(
        (createdStudent: any) => {
          this.message = 'Student created successfully!';
          this.showCreateForm = false;
          this.newStudentForm.reset(); // Reset form
          console.log('Created Student:', createdStudent);
        },
        (error) => {
          console.error('Error creating student:', error);
          this.message = 'Error creating student';
        }
      );
    } else {
      // Handle form validation errors
      this.message = 'Please fill in all required fields correctly.';
    }
  }

  deleteStudent() {
    console.warn('Please select a student to delete');
  }

  getStudent() {
    console.warn('Please select a student to retrieve');
  }
}
