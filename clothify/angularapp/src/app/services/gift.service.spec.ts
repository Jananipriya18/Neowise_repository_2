import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GiftService } from './gift.service';

describe('GiftService', () => {
  let service: GiftService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GiftService]
    });

    service = TestBed.inject(GiftService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_should_add_a_gift_when_addGift_is_called', () => {
    const gift = { 
      giftId : 1,
      giftType: 'Gift Name', 
      giftDetails: 'Gift Description',
      giftImageUrl: 'Gift Image URL',
      quantity: 10,
      giftPrice: 1000
    };
    const response = { id: '1', ...gift };
  
    (service as any).addGift(gift).subscribe();
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/gift`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(gift);
    expect(req.request.headers.get('Authorization')).toBeTruthy();
    req.flush(response); 
  });
  
  fit('Frontend_should_get_all_gifts_when_viewAllGifts_is_called', () => {
    (service as any).viewAllGifts().subscribe((gifts) => {
      expect(gifts).toBeTruthy();
    });
  
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/gift`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

  
  fit('Frontend_should_delete_a_gift_when_deleteGift_is_called', () => {
    const giftId = 1;
  
    (service as any).deleteGift(giftId).subscribe();
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/gift/${giftId}`);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

});