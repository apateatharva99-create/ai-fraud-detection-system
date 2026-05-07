import pandas as pd
from sklearn.ensemble import RandomForestClassifier
import joblib

# Simple dataset
data = {
    "amount": [100, 500, 2000, 10000, 25000, 30000],
    "hour": [10, 12, 14, 1, 2, 3],
    "is_fraud": [0, 0, 0, 0, 1, 1]
}

df = pd.DataFrame(data)

X = df[["amount", "hour"]]
y = df["is_fraud"]

model = RandomForestClassifier()
model.fit(X, y)

joblib.dump(model, "model.pkl")

print("✅ Model trained and saved as model.pkl")