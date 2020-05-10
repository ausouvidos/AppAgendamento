import axios from 'axios';
import ApiResponse from '@/models/api-response';
import Company from '@/models/company.model';

class CompanyService {
  public async add(data: Company): Promise<ApiResponse> {
    const url = '/api/Companies/Add';
    const response = await axios.post(url, data);
    return response.data;
  }

  public async getPendingApproval(): Promise<Company[]> {
    const url = '/api/Companies/PendingApproval';
    const response = await axios.get<Company[]>(url, this.getAuthOptions());
    return response.data.map((item) => new Company(item));
  }

  public async approve(data: Company, quantidade: number): Promise<ApiResponse> {
    const url = '/api/Companies/Approve';
    const response = await axios.post(url, { ...data, quantidade: +quantidade }, this.getAuthOptions());
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

export default new CompanyService();
