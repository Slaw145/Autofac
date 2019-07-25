
using Autofac;
using DIContainersLibrary.CharacterClasses;
using DIContainersLibrary.LoginPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainersLibrary
{
    public class GameServer
    {
        ILoginValidator loginvalidator;
        IPasswordValidator passwordvalidator;
        ICharacterSkillPoints characterskillpoints;
        //ICharacterRace characterrace;

        public void ResolveInterfaces(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                loginvalidator = scope.Resolve<ILoginValidator>();
                passwordvalidator = scope.Resolve<IPasswordValidator>();
                characterskillpoints = scope.Resolve<ICharacterSkillPoints>();
            }
        }

        public bool RegisterUser(string login, string password)
        {
            bool ifloginvalidate = loginvalidator.LoginValidate(login);
            bool ifpasswordvalidate = passwordvalidator.PasswordValidate(password);

            if (ifloginvalidate == true && ifpasswordvalidate == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICharacter CreateCharacter(ICharacter character)
        {
            return characterskillpoints.CreateCharacter(character);
        }

        public ICharacterRace CreateCharacterRace(ICharacterRace characterrace)
        {
            return characterrace.CreateCharacterRace(characterrace);
        }

        public bool StartGame(ICharacter character, bool ifUserIsSigned)
        {
            if (character != null && ifUserIsSigned == true)
            {
                return true;
                //Start the game
            }
            else
            {
                return false;
                //Throw exception
            }
        }
    }
}
