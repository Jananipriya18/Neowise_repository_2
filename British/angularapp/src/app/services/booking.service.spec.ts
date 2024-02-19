import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { BookingService } from './booking.service';

describe('BookingService', () => {
  let service: BookingService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [BookingService],
    });

    service = TestBed.inject(BookingService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure that there are no outstanding requests
  });

  fit('Frontend_should_add_a_booking_when_addBooking_is_called', () => {
    const booking = {
      bookingId: '1',
      noOfPersons: 5,
      fromDate: '2021-12-12',
      toDate: '2021-12-15',
      status: 'Approved',
      totalPrice: 5000,
      address: 'Tropical Island',
      userId: '1',
    };
    const response = { bookingId: '1', ...booking };

    (service as any).addBooking(booking).subscribe();

    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/booking`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(booking);
    expect(req.request.headers.get('Authorization')).toBeTruthy();
    req.flush(response);
  });

  fit('Frontend_should_call_the_API_and_get_all_bookings_when_getAllBookings_is_called', () => {
    (service as any).getAllBookings().subscribe((bookings) => {
      expect(bookings).toBeTruthy();
    });

    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/booking`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

  // fit('Frontend_should_call_the_API_and_get_bookings_by_user_id_when_getBookingsByUserId_is_called', () => {
  //   const userId = '1';
  //   (service as any).getBookingsByUserId().subscribe((bookings) => {
  //     expect(bookings).toBeTruthy();
  //   });

  //   const req = httpMock.expectOne(`${(service as any).apiUrl}/api/user/${userId}`);
  //   expect(req.request.method).toBe('GET');
  //   expect(req.request.headers.get('Authorization')).toBeTruthy();
  // });
});
