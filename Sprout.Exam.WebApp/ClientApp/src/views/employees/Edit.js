import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeEdit extends Component {
  static displayName = EmployeeEdit.name;

  constructor(props) {
    super(props);
      this.state = { id: 0, fullName: '', birthdate: '', tin: '', typeId: 1, loading: true, loadingSave: false, errors: {}  };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }
    validateFullName(fullName) {
        if (!fullName) {
            return 'Full Name is required.';
        } else if (fullName.length < 5 | fullName.length > 100) {
            return 'Full Name must be between 5 and 100 characters.';
        }
        return '';
    }

    validateBirthdate(birthdate) {
        if (!birthdate) {
            return 'Birthdate is required.';
        } else {
            const currentDate = new Date();
            const selectedDate = new Date(birthdate);

            if (selectedDate > currentDate) {
                return 'Birthdate cannot be in the future.';
            }

            const minimumBirthdate = new Date();
            minimumBirthdate.setFullYear(currentDate.getFullYear() - 18); // Minimum age of 18 years

            if (selectedDate > minimumBirthdate) {
                return 'Employee must be at least 18 years old.';
            }
        }
        return '';
    }

    validateTin(tin) {
        if (!tin) {
            return 'TIN is required.';
        } else if (tin.length !== 10) {
            return 'TIN must be exactly 10 characters.';
        }
        return '';
    }

    validateForm() {
        const errors = {
            fullName: this.validateFullName(this.state.fullName),
            birthdate: this.validateBirthdate(this.state.birthdate),
            tin: this.validateTin(this.state.tin),
        };

        this.setState({ errors });
        return Object.values(errors).every(error => error === '');
    }

  handleSubmit(e){
      e.preventDefault();

      if (this.validateForm()) {
          if (window.confirm('Are you sure you want to save?')) {
              this.saveEmployee();
          }
      }
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-6'>
  <label htmlFor='inputFullName4'>Full Name: *</label>
  <input type='text' className='form-control' id='inputFullName4' onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' />
    {this.state.errors.fullName && (
        <div className="alert alert-danger">{this.state.errors.fullName}</div>
    )}
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputBirthdate4'>Birthdate: *</label>
  <input type='date' className='form-control' id='inputBirthdate4' onChange={this.handleChange.bind(this)} name="birthdate" value={this.state.birthdate} placeholder='Birthdate' />
    {this.state.errors.birthdate && (
        <div className="alert alert-danger">{this.state.errors.birthdate}</div>
    )}
</div>
</div>
<div className="form-row">
<div className='form-group col-md-6'>
  <label htmlFor='inputTin4'>TIN: *</label>
  <input type='text' className='form-control' id='inputTin4' onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' />
    {this.state.errors.tin && (
        <div className="alert alert-danger">{this.state.errors.tin}</div>
    )}
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
  <select id='inputEmployeeType4' onChange={this.handleChange.bind(this)} value={this.state.typeId}  name="typeId" className='form-control'>
    <option value='1'>Regular</option>
    <option value='2'>Contractual</option>
  </select>
</div>
</div>
<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave?"Loading...": "Save"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Edit</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true });
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'PUT',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
        body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees/' + this.state.id,requestOptions);

    if(response.status === 200){
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
    }
    else{
        alert("There was an error occured.");
    }
  }

  async getEmployee(id) {
    this.setState({ loading: true,loadingSave: false });
    const token = await authService.getAccessToken();
    const response = await fetch('api/employees/' + id, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
      const data = await response.json();
      console.log(data);
    this.setState({ id: data.id,fullName: data.fullName,birthdate: data.birthdate,tin: data.tin,typeId: data.typeId, loading: false,loadingSave: false });
  }
}
