import React, { useState } from 'react'
import Layout from '../components/Layout'
import styles from '../styles/Rec.module.css'
import Button from "../components/Button";
import { useRouter } from "next/router"; // Router
import axios from "axios"; // axios requests

export interface IOpportuniuty {
  recName: string
  recID: string 
  assetType: String
  bedRooms: number 
  bathRooms: number
  livingAreaSqft: number
  nonLivingAreaSqft: number
  landSqft: number 
  yearBuilt: number
  asaID: number 
  assetValue: number 
  assetValueCurrency: string
  estimatedIRR: number   
  comments: string 
  addressLine1: string 
  addressLine2?: string 
  city: string   
  state: string    
  country: string      
  postalCode: string    
  ownerFirstName: string  
  ownerLastName: string   
  ownerMiddleName?: string  
  ownerPhoneNumber: string   
  ownerEmail: string        
}


  
function Opportunity() {
      const router = useRouter(); // Router navigation
      const [input, setInput] = useState({recName:"", recID:"", assetType:"", bedRooms:0, bathRooms:0, 
            livingAreaSqft:0, nonLivingAreaSqft:0, landSqft:0, yearBuilt:0, asaID:0, assetValue:0, 
            assetValueCurrency:"", estimatedIRR:0, comments:"", addressLine1:"" , addressLine2:"", 
            city:"",  state:"", country:"", postalCode:"", ownerFirstName:"", ownerLastName:"", 
            ownerMiddleName:"", ownerPhoneNumber:"", ownerEmail:"" } ) 

      
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setInput({ ...input, [e.target.name]: e.target.value })
  }

  const handleSave = async () => {
    if(!input.recName || !input.assetType ) return
    console.log("Input: " ,  {...input} )
    await axios.post("/api/opptapi", { ...input})
        .then( (response) => {
            console.log("Response:", response);
    });

  }
  
  const handleCancel = () => {
      router.push("/");
  }

  return (
    <Layout>
    <div className={styles.AddRec}>
          <h1>Asset Identification </h1>
           <hr/>
           <div>
           Real Estate Company Name:&nbsp;
           <input type="textArea" onChange={handleChange} className={styles.RecInput}
                  name="recName" value={input.recName} placeholder="recName"  /> &nbsp;
           </div>
           <hr/>
          
           <p><b>Property Details: </b></p>
         <div> 
            Asset Type:&nbsp; <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="assetType" value={input.assetType} placeholder="assetType"  /> &nbsp;
            Bed Rooms:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="bedRooms" value={input.bedRooms}  placeholder="bedRooms"  /> &nbsp;
            Bath Rooms:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="bathRooms" value={input.bathRooms}  placeholder="bathRooms"  />
          </div>
          <div> 
            Living Area Sqft:&nbsp; <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="livingAreaSqft" value={input.livingAreaSqft} placeholder="livingAreaSqft"  /> &nbsp;
            Non Living Area Sqft:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="nonLivingAreaSqft" value={input.nonLivingAreaSqft}  placeholder="nonLivingAreaSqft"  /> &nbsp;
            Land Sqft:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="landSqft" value={input.landSqft}  placeholder="landSqft"  />
          </div>
          <div> 
            Year Built:&nbsp; <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="yearBuilt" value={input.yearBuilt} placeholder="yearBuilt"  /> &nbsp;
            ASA ID:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="asaID" value={input.nonLivingAreaSqft}  placeholder="asaID"  /> &nbsp;
            Asset Value:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="assetValue" value={input.assetValue}  placeholder="assetValue"  />
          </div>
          <div> 
            Asset Value Currency:&nbsp; <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="assetValueCurrency" value={input.assetValueCurrency} placeholder="assetValueCurrency"  /> &nbsp;
            Estimated IRR:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="estimatedIRR" value={input.estimatedIRR}  placeholder="estimatedIRR"  /> &nbsp;
            Comments:&nbsp; <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="comments" value={input.comments}  placeholder="comments"  />
          </div>

          <hr/>
          <p><b>Property Address Information: </b></p>
          <div> 
            Address Line1:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="addressLine1" value={input.addressLine1} placeholder="addressLine1"  /> &nbsp;
            Line2:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="addressLine2" value={input.addressLine2}  placeholder="addressLine2"  /> &nbsp;
            City:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="city" value={input.city}  placeholder="city"  />
          </div>
          <div> 
            State:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="state" value={input.state} placeholder="state"  /> &nbsp;
            Country:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="country" value={input.country}  placeholder="country"  /> &nbsp;
            Postal Code:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="postalCode" value={input.postalCode}  placeholder="postalCode"  />
          </div>

          <hr/>
          <p><b>Property Current Owner Information: </b></p>
            
          <div> 
            First Name:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="ownerFirstName" value={input.ownerFirstName} placeholder="ownerFirstName"  /> &nbsp;
            Last Name:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="ownerLastName" value={input.ownerLastName}  placeholder="ownerLastName"  /> &nbsp;
            Middle Name&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="ownerMiddleName" value={input.ownerMiddleName}  placeholder="ownerMiddleName"  />
          </div>
          
          <div> 
            <label htmlFor="Owner Phone Number">Phone Number: </label>  &nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="ownerPhoneNumber" value={input.ownerPhoneNumber} placeholder="ownerPhoneNumber"  /> &nbsp;
            Owner Email:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="ownerEmail" value={input.ownerEmail}  placeholder="ownerEmail"  /> &nbsp;
          </div>

          <div className="line"></div>
        <br/>
        
        <div>
            <Button onClick={handleSave} > Save </Button>  
            &nbsp;&nbsp;&nbsp;&nbsp;
            <Button onClick={handleCancel}> Cancel </Button>
        </div>
     
    </div>
    </Layout>
  );
}

export default Opportunity;
