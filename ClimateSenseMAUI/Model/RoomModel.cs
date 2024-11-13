namespace ClimateSenseMAUI.Model;
// Ide: Non-nullable field.
// Me: it's going to be initialized.
// Ide: !! CS8618 !!
// Me: ↓ ↓ ↓ ↓ ↓ ↓
#pragma warning disable CS8618 

public class RoomModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public float CurrentTemperature { get; set; }
    public float CurrentHumidity { get; set; }
    
    public class Windows
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; }

        public void OpenWindow()
        {
            IsOpen = true;
        }

        public void CloseWindow()
        {
            IsOpen = false;
        }
        
    }

    public List<Windows> WindowsList = new List<Windows>();
    
    


}