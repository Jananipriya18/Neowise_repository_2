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
  message: string = '';
  newStudentForm: FormGroup;
  students: any[] = [];

  constructor(private studentService: StudentService, private fb: FormBuilder) {
    this.newStudentForm = this.fb.group({
      studentName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: [''], // Add validations as needed
      mobileNumber: ['', Validators.required],
      password: ['', Validators.required],
      UserRole: ['Student', Validators.required],
    });
  }

  ngOnInit() {
    this.fetchStudents();
  }

  fetchStudents() {
    this.studentService.getStudents().subscribe(
      (students: any[]) => {
        this.students = students;
      },
      (error) => {
        console.error('Error fetching students:', error);
      }
    );
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

          // Reset form fields
          this.newStudentForm.reset();
          // Do something with the created student data
          console.log('Created Student:', createdStudent);
          // Example: Trigger a refresh of the student list
          // this.fetchStudents();
        },
        (error) => {
          console.error('Error creating student:', error);
          if (error.status === 400 && error.error?.errors) {
            // Handle validation errors and display appropriate messages
            const validationErrors = error.error.errors;
            this.handleValidationErrors(validationErrors);
          } else {
            this.message = 'Error creating student';
          }
        }
      );
    } else {
      this.message = 'Please fill in all required fields correctly.';
    }
  }

  private handleValidationErrors(errors: any) {
    // You can customize this method based on how you want to display validation errors to the user
    console.log('Validation Errors:', errors);
  
    // For simplicity, let's assume there is a 'message' property in each error
    this.message = errors.message || 'Error creating student';
  }

  deleteStudent() {
    console.warn('Please select a student to delete');
  }

  getStudents(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/api/students`);
    console.warn('Please select a student to retrieve');

  }
}
