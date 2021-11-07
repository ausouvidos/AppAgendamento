import axios from 'axios';
import moment from 'moment';
import Availability from '@/models/availability.model';
import AvailabilityDate from '@/models/availability-date.model';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';
import CompleteAvailabilityRequest from '@/models/complete-availability-request.model';
import ApiResponse from '@/models/api-response';

class AvailabilityService {
  public async add(data: AddAvailabilitiesRequest): Promise<ApiResponse> {
    const url = '/api/Availabilities/Add';
    const response = await axios.post(url, data, this.getAuthOptions());
    return response.data;
  }

  public async complete(
    data: CompleteAvailabilityRequest,
  ): Promise<ApiResponse> {
    const url = '/api/Availabilities/Complete';
    const response = await axios.put(url, data, this.getAuthOptions());
    return response.data;
  }

  public async remove(id: number): Promise<ApiResponse> {
    const url = `/api/Availabilities/Remove/${id}`;
    const response = await axios.delete(url, this.getAuthOptions());
    return response.data;
  }

  public async getMyAvailabilities(): Promise<Availability[]> {
    const url = '/api/Availabilities/MyAvailabilities';
    const response = await axios.get<Availability[]>(
      url,
      this.getAuthOptions(),
    );
    return response.data.map((item) => new Availability(item));
  }

  public async getWeeklyAvailableSpots(
    date = new Date(),
    code: string = '',
  ): Promise<AvailabilityDate[]> {
    const url = `/api/Availabilities/WeeklyAvailableSpots/${moment(date).format(
      'YYYY-MM-DD',
    )}${code.length > 0 ? '?code=' + code : ''}`;
    const response = await axios.get<AvailabilityDate[]>(url);
    return response.data.map((item) => new AvailabilityDate(item));
  }

  public async reserveSpot(data: ReserveSpotRequest): Promise<ApiResponse> {
    const url = '/api/Availabilities/ReserveSpot';
    const response = await axios.post(url, data);
    if (response.data) {
      data.saveCache();
    }
    return response.data;
  }

  public async validateCode(code: string, email: string): Promise<ApiResponse> {
    const response = await axios.post('/api/Availabilities/ValidateSpotCode', {
        code,
        email,
    });
    return response.data;
  }

  private getAuthOptions() {
    return {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('msal.idtoken')}`,
      },
    };
  }
}

export default new AvailabilityService();
