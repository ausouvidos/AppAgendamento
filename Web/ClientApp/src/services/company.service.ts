import axios from 'axios';
import ApiResponse from '@/models/api-response';
import Company from '@/models/company.model';

class CompanyService {
  public async add(data: Company): Promise<ApiResponse> {
    const url = '/api/Companies/Add';
    const response = await axios.post(url, data);
    return response.data;
  }
}

export default new CompanyService();
