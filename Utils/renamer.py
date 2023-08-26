# New file storage didn't support names as long, and was case sensitive.
# This script will chop the names down and make them lowercase. Updates the files as well as db.

import os
import shutil
from pymongo import MongoClient

mongo_url = "xxxxxx"
folder_path = "xxxx"

def connect_to_mongodb():
    try:
        client = MongoClient(mongo_url)
        db = client.refsite
        return db.fullBodyReferences
    except Exception as error:
        print("Error connecting to MongoDB:", error)
        raise error
    
def process_documents(collection):
    mongo_records = []
    cursor = collection.find()

    for document in cursor:
        mongo_records.append({"_id": document["_id"], "File": document["File"]})

    for i, document in enumerate(mongo_records):
        doc_file = document["File"]
        print(f"Processing document for location: {doc_file}")

        filename = os.path.basename(doc_file)
        file_path = os.path.join(folder_path, filename)

        if os.path.exists(file_path):
            new_name = generate_new_filename(filename, i)
            new_path = os.path.join(folder_path, "renamed", new_name)

            shutil.copyfile(file_path, new_path)
            print(f"Renamed file to: {new_name}")

            collection.find_one_and_update(
                {"_id": document["_id"]},
                {"$set": {"File": "/images/" + new_name}}
            )
            # print("File updated in MongoDB")
        else:
            print("!!!No matching file found for location:", doc_file)

def generate_new_filename(original_name, index):
    base_name, ext = os.path.splitext(original_name)
    truncated_base_name = base_name[:75]
    new_name = f"m_f_{truncated_base_name}_{index}{ext}".lower()
    return new_name

print('Running renamer')
collection = connect_to_mongodb()
process_documents(collection)
print("Processing completed.")