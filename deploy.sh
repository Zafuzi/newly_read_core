uuid=$(uuidgen)
keepuuid=$uuid

echo -e "\nEnter password to backup the old deployment."
ssh root@newlyread.com cp -r /var/newly_read_core /var/backup/deployment_$keepuuid; 
echo -e "\ndeployment backed up"

echo -e "\nEnter password to to deploy."
ssh root@newlyread.com "cd /home/zach/git/newly_read_core; git reset --hard; dotnet restore; mkdir /var/newly_read_core; ls; dotnet publish -o /var/newly_read_core;"
echo -e "\n DEPLOYED "