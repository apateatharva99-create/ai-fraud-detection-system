from fastapi import FastAPI
from pydantic import BaseModel
import joblib
import pandas as pd

app = FastAPI()

# ✅ Request model (THIS FIXES SWAGGER)
class Transaction(BaseModel):
    amount: float
    time: str

# Load model
model = joblib.load("model.pkl")

@app.get("/")
def home():
    return {"message": "AI Fraud Detection API Running"}

@app.post("/predict")
def predict(data: Transaction):
    amount = data.amount
    hour = pd.to_datetime(data.time).hour

    input_data = pd.DataFrame([[amount, hour]], columns=["amount", "hour"])

    prediction = model.predict(input_data)[0]
    probability = model.predict_proba(input_data)[0][1]

    return {
        "is_fraud": int(prediction),
        "fraud_score": float(probability)
    }