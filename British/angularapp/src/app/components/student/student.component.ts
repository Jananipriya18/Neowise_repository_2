// // student.component.ts

// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { Observable } from 'rxjs';
// import { StudentService } from '../../services/student.service';

// @Component({
//   selector: 'app-student',
//   templateUrl: './student.component.html',
//   styleUrls: ['./student.component.css']
// })
// export class StudentComponent implements OnInit {
//   showCreateForm: boolean = false;
//   message: string = '';
//   newStudentForm: FormGroup;
//   students: any[] = [];  // Add this line to declare the 'students' array

//   constructor(private studentService: StudentService, private fb: FormBuilder) {
//     this.newStudentForm = this.fb.group({
//       studentName: ['', Validators.required],
//       email: ['', [Validators.required, Validators.email]],
//       username: [''], // Add validations as needed
//       mobileNumber: ['', Validators.required],
//       password: ['', Validators.required],
//       UserRole: ['Student', Validators.required],
//     });
//   }

//   ngOnInit() {
//     this.fetchStudents();
//   }

//   fetchStudents() {
//     this.studentService.getStudents().subscribe(
//       (students: any[]) => {
//         this.students = students;
//       },
//       (error) => {
//         console.error('Error fetching students:', error);
//       }
//     );
//   }

//   toggleCreateForm() {
//     this.showCreateForm = !this.showCreateForm;
//   }

//   createStudent() {
//     if (this.newStudentForm.valid) {
//       this.studentService.createStudent(this.newStudentForm.value).subscribe(
//         (createdStudent: any) => {
//           window.alert('Student created successfully!');
//           this.message = 'Student created successfully!';
//           this.showCreateForm = false;

//           // Reset form fields
//           this.newStudentForm.reset();
//           // Do something with the created student data
//           console.log('Created Student:', createdStudent);
//           // Example: Trigger a refresh of the student list
//           this.fetchStudents();  // Add this line to refresh the student list
//         },
//         (error) => {
//           console.error('Error creating student:', error);
//           // if (error.status === 400 && error.error?.errors) {
//           //   // Handle validation errors and display appropriate messages
//           //   const validationErrors = error.error.errors;
//           //   this.handleValidationErrors(validationErrors);
//           // } else {
//           //   this.message = 'Error creating student';
//           // }

//           if (error.error.Status === "Error") {
//             window.alert('Student creation is not successful!');
//               // Username already exists
//               alert(error.error.Message);
//             }
//         }
//       );
//     } else {
//       this.message = 'Please fill in all required fields correctly.';
//     }
//   }

//   isPasswordComplex(password: string): boolean {
//     const hasUppercase = /[A-Z]/.test(password);
//     const hasLowercase = /[a-z]/.test(password);
//     const hasDigit = /\d/.test(password);
//     const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password);

//     return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
//   }

//   private handleValidationErrors(errors: any) {
//     // You can customize this method based on how you want to display validation errors to the user
//     console.log('Validation Errors:', errors);

//     // For simplicity, let's assume there is a 'message' property in each error
//     this.message = errors.message || 'Error creating student';
//   }

//   deleteStudent() {
//     console.warn('Please select a student to delete');
//   }

//   getStudent(userId: number) {
//     this.studentService.getStudent(userId).subscribe(
//       (student: any) => {
//         if (student) {
//           // Do something with the retrieved student data
//           console.log('Retrieved Student:', student);
//         } else {
//           console.warn('No student found with the given ID.');
//         }
//       },
//       (error) => {
//         console.error('Error fetching student:', error);
//       }
//     );
//   }
  

//   // Add this method to retrieve all students
//   getStudents() {
//     this.studentService.getStudents().subscribe(
//       (students: any[]) => {
//         this.students = students;
//       },
//       (error) => {
//         console.error('Error fetching students:', error);
//       }
//     );
//   }
// }

// student.component.ts

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  showCreateForm: boolean = false;
  message: string = '';
  newStudentForm: FormGroup;
  students: any[] = [];
  userId: number;
  selectedStudent: any;

  constructor(private studentService: StudentService, private fb: FormBuilder) {
    this.newStudentForm = this.fb.group({
      studentName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: [''],
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

  // toggleCreateForm() {
  //   this.showCreateForm = !this.showCreateForm;
  // }

  // createStudent() {
  //   if (this.newStudentForm.valid) {
  //     this.studentService.createStudent(this.newStudentForm.value).subscribe(
  //       (createdStudent: any) => {
  //         window.alert('Student created successfully!');
  //         this.message = 'Student created successfully!';
  //         this.showCreateForm = false;

  //         // Reset form fields
  //         this.newStudentForm.reset();
  //         // Do something with the created student data
  //         console.log('Created Student:', createdStudent);
  //         // Example: Trigger a refresh of the student list
  //         this.fetchStudents();  // Add this line to refresh the student list
  //       },
  //       (error) => {
  //         console.error('Error creating student:', error);

  //         if (error.error.Status === "Error") {
  //           window.alert('Student creation is not successful!');
  //           alert(error.error.Message);
  //         }
  //       }
  //     );
  //   } else {
  //     this.message = 'Please fill in all required fields correctly.';
  //   }
  // }

   toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
  }

  createStudent() {
    if (this.newStudentForm.valid) {
      this.studentService.createStudent(this.newStudentForm.value).subscribe(
        (createdStudent: any) => {
          window.alert('Student created successfully!');
          this.message = 'Student created successfully!';
          this.showCreateForm = false;

          // Reset form fields
          this.newStudentForm.reset();
          // Do something with the created student data
          console.log('Created Student:', createdStudent);
          // Example: Trigger a refresh of the student list
          this.fetchStudents();  // Add this line to refresh the student list
        },
        (error) => {
          console.error('Error creating student:', error);

          if (error.error.Status === "Error") {
            window.alert('Student creation is not successful!');
            alert(error.error.Message);
          }
        }
      );
    } else {
      this.message = 'Please fill in all required fields correctly.';
    }
  }

  isPasswordComplex(password: string): boolean {
    const hasUppercase = /[A-Z]/.test(password);
    const hasLowercase = /[a-z]/.test(password);
    const hasDigit = /\d/.test(password);
    const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password);

    return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
  }

  private handleValidationErrors(errors: any) {
    console.log('Validation Errors:', errors);
    this.message = errors.message || 'Error creating student';
  }

  deleteStudent() {
    console.warn('Please select a student to delete');
  }

  getStudent(userId: number) {
    this.studentService.getStudent(userId).subscribe(
      (student: any) => {
        if (student) {
          console.log('Retrieved Student:', student);
        } else {
          console.warn('No student found with the given ID.');
        }
      },
      (error) => {
        console.error('Error fetching student:', error);
      }
    );
  }

  getStudents() {
    this.studentService.getStudents().subscribe(
      (students: any[]) => {
        this.students = students;
      },
      (error) => {
        console.error('Error fetching students:', error);
      }
    );
  }

  getStudentDetails() {
    if (!this.userId) {
      window.alert('Please enter a valid user ID.');
      return;
    }

    this.studentService.getStudent(this.userId).subscribe(
      (student: any) => {
        if (student) {
          this.selectedStudent = student;
          console.log('Retrieved Student:', student);
        } else {
          console.warn('No student found with the given ID.');
        }
      },
      (error) => {
        console.error('Error fetching student:', error);
      }
    );
  }
}
