import { Component } from '@angular/core';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent {
  showCreateForm: boolean = false;
  message: string = '';
  newStudent: any = {
    studentName: '',
    mobileNumber: '',
    email:'',
    password:'',
    UserRole:'Student'

  };

  constructor(private studentService: StudentService) {}

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
  }

  createStudent() {
    this.studentService.createStudent(this.newStudent).subscribe(
      (createdStudent: any) => {
        this.message = 'Student created successfully!';
        this.showCreateForm = false;
        // Reset form fields
        this.newStudent = { studentName: '', mobileNumber: '' };

        // Do something with the created student data
        console.log('Created Student:', createdStudent);

        // Example: Trigger a refresh of the student list
        // this.fetchStudents();
      },
      (error) => {
        console.error('Error creating student:', error);
        this.message = 'Error creating student';
      }
    );
  }

  deleteStudent() {
    // You may want to implement some form of user interaction to select a student for deletion
    console.warn('Please select a student to delete');
  }

  getStudent() {
    // You may want to implement some form of user interaction to select a student for retrieval
    console.warn('Please select a student to retrieve');
  }
}
