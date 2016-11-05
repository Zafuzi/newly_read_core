uuid=$(uuidgen)
keepuuid=$uuid

echo -e "\nEnter password to backup the old deployment."
ssh root@newlyread.com "cp -r /var/newly_read_core /var/backup/deployment_$keepuuid; rm -rf /var/newly_read_core; mkdir /var/newly_read_core"; 
echo -e "\ndeployment backed up"

echo -e "\nEnter password to to deploy."
ssh zach@newlyread.com "cd ./git/newly_read_core; git fetch; git reset --hard origin/master;"
ssh root@newlyread.com "cd /home/zach/git/newly_read_core; dotnet publish -o /var/newly_read_core; nohup dotnet run /var/newly_read_core/newly_read_core.dll;"